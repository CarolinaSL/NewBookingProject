using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using NewBookingApp.Core.CQRS;
using NewBookingProject.Passenger.API.Data;
using NewBookingProject.Passenger.API.DTOs;

namespace NewBookingProject.Passenger.API.Commands.CompleteRegisterPassenger
{
    public class CompleteRegisterPassengerCommandHandler : ICommandHandler<CompleteRegisterPassengerCommand, PassengerResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly PassengerDbContext _passengerDbContext;

        public CompleteRegisterPassengerCommandHandler(IMapper mapper, PassengerDbContext passengerDbContext)
        {
            _mapper = mapper;
            _passengerDbContext = passengerDbContext;
        }

        public async Task<PassengerResponseDto> Handle(CompleteRegisterPassengerCommand command, CancellationToken cancellationToken)
        {
            //verifica se passageiro já foi cadastrado 
            var passenger = await _passengerDbContext.Passengers.AsNoTracking()
                .SingleOrDefaultAsync(x => x.PassportNumber == command.PassportNumber, cancellationToken);

            if (passenger is null)
                throw new NotImplementedException();

            var passengerEntity = passenger.CompleteRegistrationPassenger(passenger.Id, passenger.Name, passenger.PassportNumber, command.PassengerType, command.Age);

            var updatePassenger = _passengerDbContext.Passengers.Update(passengerEntity);

           await _passengerDbContext.SaveChangesAsync();

            return _mapper.Map<PassengerResponseDto>(updatePassenger.Entity);
        }
    }

}
