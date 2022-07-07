using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NewBookingApp.Flight.Domain.Aircrafts.Models;
using NewBookingApp.Flight.Domain.Airports.Models;
using NewBookingApp.Flight.Domain.Seats.Models;
using System.Reflection;

namespace NewBookingApp.Flight.Infra.Context
{
    public class FlightDbContext : DbContext
    {
        public FlightDbContext(DbContextOptions<FlightDbContext> options, IHttpContextAccessor httpContextAccessor) : base(
             options)
        {
        }

        public DbSet<Domain.Flights.Models.Flight> Flights => Set<Domain.Flights.Models.Flight>();
        public DbSet<Airport> Airports => Set<Airport>();
        public DbSet<Aircraft> Aircraft => Set<Aircraft>();
        public DbSet<Seat> Seats => Set<Seat>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

    }
}
