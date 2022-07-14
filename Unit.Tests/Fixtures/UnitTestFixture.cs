using MapsterMapper;
using NewBookingApp.Flight.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit.Tests.Helpers;
using Xunit;

namespace Unit.Tests.Fixtures
{
    [CollectionDefinition(nameof(UnitTestFixture))]
    public class FixtureCollection : ICollectionFixture<UnitTestFixture> { }

    public class UnitTestFixture : IDisposable 
    {
        public FlightDbContext DbContext { get; }
        public IMapper Mapper { get; }
        public UnitTestFixture()
        {
            IdGeneratorFactory.Create();
            Mapper = MapperFactory.Create();
            DbContext = DbContextFactory.Create();
        }

        public void Dispose()
        {
            DbContextFactory.Destroy(DbContext);
        }
    }
}
