using Mapster;
using MapsterMapper;
using NewBookingProject.Flight.API;

namespace Unit.Tests.Helpers
{
    public static class MapperFactory
    {
        public static IMapper Create()
        {
            var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
            typeAdapterConfig.Scan(typeof(FlightRoot).Assembly);
            IMapper instance = new Mapper(typeAdapterConfig);

            return instance;
        }
    }
}
