using NewBookingApp.Core.CQRS;
using NewBookingApp.Flight.API.Dtos;

namespace NewBookingApp.Flight.API.Queries.GetAvailableFlights
{
    public record GetAvailableFlightsQuery : IQuery<IEnumerable<FlightResponseDto>>
    {

    }
}
