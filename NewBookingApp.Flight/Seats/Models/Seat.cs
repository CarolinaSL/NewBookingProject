using NewBookingApp.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBookingApp.Flight.Domain.Seats.Models
{
    public class Seat : Aggregate<long>
    {
        public string SeatNumber { get; private set; }
        public SeatType Type { get; private set; }
        public SeatClass Class { get; private set; }
        public long FlightId { get; private set; }

        public static Seat Create(long id, string seatNumber, SeatType type, SeatClass @class, long flightId)
        {
            var seat = new Seat()
            {
                Id = id,
                Class = @class,
                Type = type,
                SeatNumber = seatNumber,
                FlightId = flightId
            };

            return seat;
        }

        public Task<Seat> ReserveSeat(Seat seat)
        {
            seat.IsDeleted = true;
            seat.LastModified = DateTime.Now;
            return Task.FromResult(this);
        }

    }

}
