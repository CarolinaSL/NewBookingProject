using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewBookingApp.Flight.Domain.Aircrafts.Models;
using NewBookingApp.Flight.Domain.Airports.Models;

namespace NewBookingApp.Flight.Infra.Configurations
{
    public class FlightConfiguration : IEntityTypeConfiguration<Domain.Flights.Models.Flight>
    {
        public void Configure(EntityTypeBuilder<Domain.Flights.Models.Flight> builder)
        {
            builder.ToTable("Flight");

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedNever();

            builder
                .HasOne<Aircraft>()
                .WithMany()
                .HasForeignKey(p => p.AircraftId);

            builder
                .HasOne<Airport>()
                .WithMany()
                .HasForeignKey(d => d.DepartureAirportId)
                .HasForeignKey(a => a.ArriveAirportId);


        }
    }
}
