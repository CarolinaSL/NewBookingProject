using Mapster;
using NewBookingApp.Flight.API.Dtos;
using NewBookingApp.Flight.API.Queries.GetFlightById;

namespace NewBookingApp.Flight.API.Mappings
{
    public class FlightMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Domain.Flights.Models.Flight, FlightResponseDto>();
            config.NewConfig<GetFlightByIdQuery, FlightResponseDto>();

        }
    }
}
