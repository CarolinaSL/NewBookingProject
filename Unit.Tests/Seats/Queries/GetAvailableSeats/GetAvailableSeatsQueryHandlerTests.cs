using MapsterMapper;
using MediatR;
using Moq;
using NewBookingApp.Core.CQRS;
using NewBookingApp.Flight.API.Dtos;
using NewBookingApp.Flight.API.Queries.GetAvailableSeats;
using NewBookingApp.Flight.Domain.Seats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit.Tests.Fixtures;
using Xunit;

namespace Unit.Tests.GetAvailableSeats
{
    [Collection(nameof(UnitTestFixture))]
    public class GetAvailableSeatsQueryHandlerTests
    {
        private readonly UnitTestFixture _fixture;
        private readonly GetAvailableSeatsQueryHandler _handler;
        private readonly Mock<IMapper> _mapperMock;


        public GetAvailableSeatsQueryHandlerTests(UnitTestFixture fixture)
        {
            _fixture = fixture;
            _handler = new GetAvailableSeatsQueryHandler(_fixture.Mapper, fixture.DbContext);
        }


        public Task<IEnumerable<SeatResponseDto>> Act(GetAvailableSeatsQuery query, CancellationToken cancellationToken)
        {
            return _handler.Handle(query, cancellationToken);
        }

        [Fact]
        public async Task passValidQueryToHandlerShouldReturnListOfAvailableSeats()
        {
            // Arrange
            var query = new GetAvailableSeatsQuery(1);
           
            //act
            var response = await Act(query, CancellationToken.None);

            //assert
            Assert.NotNull(response);
            Assert.IsAssignableFrom<IEnumerable<SeatResponseDto>>(response);
            Assert.All(response, x => Assert.Contains(response, x => x.FlightId == 1));
        }




    }
}
