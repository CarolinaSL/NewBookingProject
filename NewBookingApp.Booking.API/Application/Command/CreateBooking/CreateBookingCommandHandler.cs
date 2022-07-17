using Mapster;
using MassTransit;
using NewBookingApp.Booking.API.DTOs;
using NewBookingApp.Booking.Domain.Interfaces;
using NewBookingApp.Booking.Domain.Models.ValueObjects;
using NewBookingApp.Core.Contracts;
using NewBookingApp.Core.CQRS;
using NewBookingApp.Core.Exceptions;

namespace NewBookingApp.Booking.API.Command.CreateBooking
{

    public class CreateBookingCommandHandler : ICommandHandler<CreateBookingCommand, CreateReservationResponseDto>
    {
        private readonly IRequestClient<GetFlightById> _clientA;
        private readonly IRequestClient<GetAvailabeSeatsbyId> _clientB;
        private readonly IRequestClient<GetPassengerByIdRequest> _clientC;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IBookingRepository _repository;

        public CreateBookingCommandHandler( 
            IRequestClient<GetFlightById> clientA,
            IRequestClient<GetAvailabeSeatsbyId> clientB,
            IRequestClient<GetPassengerByIdRequest> clientC,
            ISendEndpointProvider sendEndpointProvider, 
            IBookingRepository repository)
        {
            
            _clientA = clientA;
            _clientB = clientB;
            _clientC = clientC;
            _sendEndpointProvider = sendEndpointProvider;
            _repository = repository;
        }

        public async Task<CreateReservationResponseDto> Handle(CreateBookingCommand command,
       CancellationToken cancellationToken)
        {
            var flightMessage= await _clientA.GetResponse<FlightResponse>(new { FlightId = command.FlightId },cancellationToken);

            var flight = flightMessage.Message;
            if (flightMessage is null)
                throw new NotImplementedException();

            var emptySeatMessage = await _clientB.GetResponse<SeatResponse>(new { FlightId = command.FlightId }, cancellationToken);

            var emptySeat = emptySeatMessage.Message;

            var passengerMessage =await _clientC.GetResponse<PassengerResponse>(new { PassengerId = command.PassengerId }, cancellationToken);

            var passenger = passengerMessage.Message;

          //  await Task.WhenAll(flightMessage, emptySeatMessage, passengerMessage);

            //var flight = flightMessage.Result.Message;
           // var emptySeat = emptySeatMessage.Result.Message;
           // var passenger = passengerMessage.Result.Message;

            var reservation = await _repository.GetById(command.Id);

            if (reservation is not null && !reservation.IsDeleted)
                throw new BookingAlreadyExistException();
          

            var aggregate = Domain.Models.Booking.Create(command.Id, new PassengerInfo(passenger.Name), new Trip(
            flight.FlightNumber, flight.AircraftId, flight.DepartureAirportId,
            flight.ArriveAirportId, flight.FlightDate, flight.Price, command.Description, emptySeat?.SeatNumber));

            var _serviceAddress = "queue:ReserveSeat";
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(_serviceAddress));

            await endpoint.Send(new ReserveSeatRequestDto
            {
                FlightId = flight.Id,
                SeatNumber = emptySeat?.SeatNumber
            });

             _repository.Add(aggregate);

            var result = await _repository.UnitOfWork.Commit();

            var reservationResponseDto = aggregate.Adapt<CreateReservationResponseDto>();

            if (result is true)
            {
                var _serveceAddressEmail = "queue:SendEmail";
                var endpointEmail = await _sendEndpointProvider.GetSendEndpoint(new Uri(_serveceAddressEmail));

                await endpointEmail.Send(new SendEmailRequestDto
                {
                    PassengerName = passenger.Name,
                    PassengerPassport = passenger.PassportNumber,
                    FlightNumber = reservationResponseDto.FlightNumber,
                    FlightDate = reservationResponseDto.FlightDate,
                    SeatNumber = reservationResponseDto.SeatNumber,

                });
            }
           

            return reservationResponseDto;
        }
    }
}
