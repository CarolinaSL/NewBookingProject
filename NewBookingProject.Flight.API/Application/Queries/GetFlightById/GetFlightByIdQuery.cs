using NewBookingApp.Core.CQRS;
using NewBookingApp.Flight.API.Dtos;

namespace NewBookingApp.Flight.API.Queries.GetFlightById
{
    public record GetFlightByIdQuery(long Id) : IQuery<FlightResponseDto>;
}
