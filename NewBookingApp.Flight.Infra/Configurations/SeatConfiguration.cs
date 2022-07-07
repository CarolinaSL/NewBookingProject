using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewBookingApp.Flight.Domain.Seats.Models;

namespace NewBookingApp.Flight.Infra.Configurations
{
    public class SeatConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            builder.ToTable("Seat");

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedNever();

            builder
                .HasOne<Domain.Flights.Models.Flight>()
                .WithMany()
                .HasForeignKey(p => p.FlightId);
        }
    }

}
