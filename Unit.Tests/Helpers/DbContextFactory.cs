using Microsoft.EntityFrameworkCore;
using NewBookingApp.Flight.Infra.Context;
using NewBookingApp.Flight.Domain.Flights.Models;
using NewBookingApp.Flight.Domain.Aircrafts.Models;
using NewBookingApp.Flight.Domain.Airports.Models;
using NewBookingApp.Flight.Domain.Seats.Models;

namespace Unit.Tests.Helpers
{
    public static class DbContextFactory
    {

        public static FlightDbContext Create()
        {
            var options = new DbContextOptionsBuilder<FlightDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var context = new FlightDbContext(options);

            // Seed our data
            FlightDataSeeder(context);

            return context;
        }

        private static void FlightDataSeeder(FlightDbContext context)
        {
            var airports = new List<Airport>
            {
              Airport.Create(1, "Lisbon International Airport", "LIS", "12988"),
              Airport.Create(2, "Sao Paulo International Airport", "BRZ", "11200")
            };

            context.Airports.AddRange(airports);

            var aircrafts = new List<NewBookingApp.Flight.Domain.Aircrafts.Models.Aircraft>
            {
                Aircraft.Create(1, "Boeing 737", "B737"),
                Aircraft.Create(2, "Airbus 300", "A300"),
                Aircraft.Create(3, "Airbus 320", "A320")
            };

            context.Aircraft.AddRange(aircrafts);

            var flights = new List<Flight>
            {
                Flight.Create(1, "BD467", 1, 1, new DateTime(2022, 1, 31, 12, 0, 0),
                    new DateTime(2022, 1, 31, 14, 0, 0),
                    2, 120m,
                    new DateTime(2022, 1, 31), FlightStatus.Completed,
                    8000)
            };
            context.Flights.AddRange(flights);

            var seats = new List<Seat>
            {
                Seat.Create(1, "12A", SeatType.Window, SeatClass.Economy, 1),
                Seat.Create(2, "12B", SeatType.Window, SeatClass.Economy, 1),
                Seat.Create(3, "12C", SeatType.Middle, SeatClass.Economy, 1),
                Seat.Create(4, "12D", SeatType.Middle, SeatClass.Economy, 1),
                Seat.Create(5, "12E", SeatType.Aisle, SeatClass.Economy, 1),
                Seat.Create(6, "12F", SeatType.Aisle, SeatClass.Economy, 1)
            };

            context.Seats.AddRange(seats);

            context.SaveChanges();
        }

        public static void Destroy(FlightDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
