using Mapster;
using NewBookingApp.Flight.API.Dtos;
using NewBookingApp.Flight.Domain.Seats.Models;

namespace NewBookingApp.Flight.API.Mappings
{
    public class SeatMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Seat, SeatResponseDto>();
        }
    }
}
