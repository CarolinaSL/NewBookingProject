﻿using MassTransit;
using NewBookingApp.Booking.Domain.Interfaces;
using NewBookingApp.Booking.Domain.Models.ValueObjects;
using NewBookingApp.Booking.Infra.Respository;
using NewBookingApp.Core.Contracts;
using NewBookingApp.Core.CQRS;
using NewBookingApp.Core.Exceptions;

namespace NewBookingApp.Booking.API.Command.CreateBooking
{

    public class CreateBookingCommandHandler : ICommandHandler<CreateBookingCommand, ulong>
    {
        private readonly IBus _bus;
        IRequestClient<GetFlightById> _clientA;
        IRequestClient<GetAvailabeSeatsbyId> _clientB;
        private readonly IRequestClient<GetPassengerById> _clientC;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IBookingRepository _repository;

        public CreateBookingCommandHandler( 
            IRequestClient<GetFlightById> clientA,
            IRequestClient<GetAvailabeSeatsbyId> clientB,
            IRequestClient<GetPassengerById> clientC,
            ISendEndpointProvider sendEndpointProvider, 
            IBookingRepository repository)
        {
            
            _clientA = clientA;
            _clientB = clientB;
            _clientC = clientC;
            _sendEndpointProvider = sendEndpointProvider;
            _repository = repository;
        }

        public async Task<ulong> Handle(CreateBookingCommand command,
       CancellationToken cancellationToken)
        {
            var flightMessage= _clientA.GetResponse<FlightResponse>(new { FlightId = command.FlightId },cancellationToken);

            if (flightMessage is null)
                throw new NotImplementedException();

            var emptySeatMessage = _clientB.GetResponse<SeatResponse>(new { FlightId = command.FlightId }, cancellationToken);

            var passengerMessage = _clientC.GetResponse<PassengerResponse>(new { PassengerId = command.PassengerId });

            await Task.WhenAll(flightMessage, emptySeatMessage, passengerMessage);

            var flight = flightMessage.Result.Message;
            var emptySeat = emptySeatMessage.Result.Message;
            var passenger = passengerMessage.Result.Message;

            var reservation = await _repository.GetById(command.Id);

            if (reservation is not null && !reservation.IsDeleted)
                throw new BookingAlreadyExistException();
          

            var aggregate = Domain.Models.Booking.Create(command.Id, new PassengerInfo(passenger.Name), new Trip(
            flight.FlightNumber, flight.AircraftId, flight.DepartureAirportId,
            flight.ArriveAirportId, flight.FlightDate, flight.Price, command.Description, emptySeat?.SeatNumber));

            var _serviceAddress = "exchange:flight";
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(_serviceAddress));

            await endpoint.Send(new ReserveSeatRequestDto
            {
                FlightId = flight.Id,
                SeatNumber = emptySeat?.SeatNumber
            });

             _repository.Add(aggregate);

            await _repository.UnitOfWork.Commit();

            return 1;
        }
    }
}
