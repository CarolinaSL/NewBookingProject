using NewBookingApp.Core.CQRS;
using NewBookingApp.Flight.API.Dtos;

namespace NewBookingApp.Flight.API.Queries.GetAvailableSeats
{
    public record GetAvailableSeatsQuery(long FlightId) : IQuery<IEnumerable<SeatResponseDto>>;
}
