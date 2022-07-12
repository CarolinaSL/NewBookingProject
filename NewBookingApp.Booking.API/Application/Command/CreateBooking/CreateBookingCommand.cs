using NewBookingApp.Booking.API.DTOs;
using NewBookingApp.Core.CQRS;
using NewBookingApp.Core.Generators;

namespace NewBookingApp.Booking.API.Command.CreateBooking
{
    public record CreateBookingCommand(long PassengerId, long FlightId, string Description) : ICommand<CreateReservationResponseDto>
    {
        public long Id { get; set; } = SnowFlakIdGenerator.NewId();
    }
}
