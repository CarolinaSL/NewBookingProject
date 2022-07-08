using MassTransit;
using NewBookingApp.Core.Contracts;
using NewBookingApp.Core.Generators;
using NewBookingProject.Passenger.API.Data;

namespace NewBookingProject.Passenger.API.Identity
{
    public class RegisterNewUserConsumerHandler : IConsumer<UserCreated>
    {

        private readonly PassengerDbContext _passengerDbContext;

        public RegisterNewUserConsumerHandler(PassengerDbContext passengerDbContext)
        {
            _passengerDbContext = passengerDbContext;
        }

        public async Task Consume(ConsumeContext<UserCreated> context)
        {
            var passenger = Passengers.Models.Passenger.Create(SnowFlakIdGenerator.NewId(), context.Message.Name, context.Message.PassportNumber);

            await _passengerDbContext.AddAsync(passenger);

            await _passengerDbContext.SaveChangesAsync();
        }
    }
}
