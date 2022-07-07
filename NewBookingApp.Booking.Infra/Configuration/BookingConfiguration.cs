using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBookingApp.Booking.Infra.Configuration
{
    public class BookingConfiguration : IEntityTypeConfiguration<Domain.Models.Booking>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Booking> builder)
        {
            builder.ToTable("Booking");

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedNever();

            builder.OwnsOne(c => c.Trip, x =>
            {
                x.Property(c => c.Description);
                x.Property(c => c.Price);
                x.Property(c => c.AircraftId);
                x.Property(c => c.FlightDate);
                x.Property(c => c.FlightNumber);
                x.Property(c => c.SeatNumber);
                x.Property(c => c.ArriveAirportId);
                x.Property(c => c.DepartureAirportId);
            });

            builder.OwnsOne(c => c.PassengerInfo, x =>
            {
                x.Property(c => c.Name);
            });
        }
    }

}
