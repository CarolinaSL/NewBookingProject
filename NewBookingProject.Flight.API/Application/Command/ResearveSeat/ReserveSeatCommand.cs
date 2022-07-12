using NewBookingApp.Core.CQRS;
using NewBookingApp.Flight.API.Dtos;

namespace NewBookingApp.Flight.API.Command.ResearveSeat
{
    public record ReserveSeatCommand(long FlightId, string SeatNumber) : ICommand<SeatResponseDto>;
}
