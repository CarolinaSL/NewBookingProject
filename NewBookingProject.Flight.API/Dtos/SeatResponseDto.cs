using NewBookingApp.Flight.Domain.Seats.Models;

namespace NewBookingApp.Flight.API.Dtos
{
    public record SeatResponseDto
    {
        public long Id { get; set; }
        public string SeatNumber { get; init; }
        public SeatType Type { get; init; }
        public SeatClass Class { get; init; }
        public long FlightId { get; init; }
    }
}
