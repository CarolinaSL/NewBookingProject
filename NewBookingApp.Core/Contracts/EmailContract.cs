using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBookingApp.Core.Contracts
{
    public record SendEmailRequestDto
    {
        public string PassengerName { get; init; }
        public string PassengerPassport { get; init; }
        public string FlightNumber { get; init; }
        public DateTime FlightDate { get; init; }
        public string SeatNumber { get; init; }
    }

    public record RequestUserByPassportNumber(string passportNumber);

    public record GetUserResponse
    {
        public string PassengerEmail { get; init; }
    }

}
