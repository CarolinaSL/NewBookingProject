using NewBookingApp.Booking.Domain.Models.ValueObjects;
using NewBookingApp.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBookingApp.Booking.Domain.Models
{
    public class Booking : Aggregate<long>
    {
        public Booking()
        {
        }

        public Trip Trip { get; private set; }
        public PassengerInfo PassengerInfo { get; private set; }

        public static Booking Create(long id, PassengerInfo passengerInfo, Trip trip, bool isDeleted = false)
        {
            var booking = new Booking()
            {
                Id = id,
                Trip = trip,
                PassengerInfo = passengerInfo,
                IsDeleted = isDeleted
            };

           
            return booking;
        }
    }

}
