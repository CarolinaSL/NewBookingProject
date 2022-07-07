using NewBookingApp.Core.CQRS;
using NewBookingApp.Core.Generators;

namespace NewBookingApp.Booking.API.Command.CreateBooking
{
    public record CreateBookingCommand(long PassengerId, long FlightId, string Description) : ICommand<ulong>
    {
        public long Id { get; set; } = SnowFlakIdGenerator.NewId();
    }
}
