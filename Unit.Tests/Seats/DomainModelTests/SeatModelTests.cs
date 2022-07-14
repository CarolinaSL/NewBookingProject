using MassTransit.Mediator;
using Moq;
using NewBookingApp.Core.CQRS;
using NewBookingApp.Flight.API.Command.ResearveSeat;
using NewBookingApp.Flight.API.Dtos;
using NewBookingApp.Flight.Domain.Seats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit.Tests.Fixtures;
using Xunit;

namespace Unit.Tests.Seats.DomainModelTests
{
    [Collection(nameof(UnitTestFixture))]
    public class SeatModelTests
    {
        private UnitTestFixture _fixture;

        public SeatModelTests(UnitTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async Task reserveSeatShouldChangeFlagToTrue()
        {
            // arrange
            var teste = await _fixture.DbContext.Seats.FindAsync((long)1);

            //act
            var result = teste.ReserveSeat(teste).Result;
            //assert

            Assert.NotNull(result);
            Assert.IsType<Seat>(result);
            Assert.True(result.IsDeleted);
            Assert.NotNull(result.LastModified);

        }

        [Fact]
        public async Task reserveSeatCommandHandlerShouldReturnSeatResponseDto()
        {
            // arrange
            var commandHandler = new ReserveSeatCommandHandler(_fixture.Mapper, _fixture.DbContext);

            
            var seat = await _fixture.DbContext.Seats.FindAsync((long)1);
            var command = new ReserveSeatCommand(seat.FlightId, seat.SeatNumber);
            //act

            var result = commandHandler.Handle(command, CancellationToken.None).Result;
            var modifiedSeat = await _fixture.DbContext.Seats.FindAsync(result.Id);

            //assert
            Assert.NotNull(result);
            Assert.IsType<SeatResponseDto>(result);
            Assert.True(modifiedSeat.IsDeleted);
            Assert.NotNull(modifiedSeat.LastModified);

        }

    }
}
