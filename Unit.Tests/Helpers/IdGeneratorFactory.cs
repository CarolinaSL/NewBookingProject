using NewBookingApp.Core.Generators;

namespace Unit.Tests.Helpers
{
    public static class IdGeneratorFactory
    {

        public static void Create()
        {
            SnowFlakIdGenerator.Configure(1);
        }
    }
}
