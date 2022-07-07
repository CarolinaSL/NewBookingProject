using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBookingApp.Core.Contracts
{
    public record GetFlightById 
    {
        public long FlightId { get; init; }
    }

    public record GetAvailabeSeatsbyId
    {
        public long FlightId { get; init; }
    }

    public record FlightResponse
    {
     
        public long Id { get; init; }
        public string FlightNumber { get; init; }
       
        public long AircraftId { get; init; }
      
        public long DepartureAirportId { get; init; }
     
        public DateTime DepartureDate { get; init; }
     
        public DateTime ArriveDate { get; init; }
    
        public long ArriveAirportId { get; init; }
   
        public decimal DurationMinutes { get; init; }

        public DateTime FlightDate { get; init; }
 
        public FlightStatus Status { get; init; }
     
        public decimal Price { get; init; }
    }

    public enum FlightStatus
    {
        Flying = 1,
        Delay = 2,
        Canceled = 3,
        Completed = 4
    }
    public record SeatResponse
    {
        public long Id { get; set; }
        public string SeatNumber { get; init; }
        public SeatType Type { get; init; }
        public SeatClass Class { get; init; }
        public long FlightId { get; init; }
    }

    public enum SeatType
    {
        Window,
        Middle,
        Aisle
    }

    public enum SeatClass
    {
        FirstClass,
        Business,
        Economy
    }

    public record ReserveSeatRequestDto
    {
    
        public long FlightId { get; init; }
        public string SeatNumber { get; init; }
    }


}
