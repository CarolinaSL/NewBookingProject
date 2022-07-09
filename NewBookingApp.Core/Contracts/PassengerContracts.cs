using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBookingApp.Core.Contracts
{
    public record GetPassengerByIdRequest
    {
        public long Id { get; init; }
    }

    public record PassengerResponse
    {
        public long Id { get; init; }
        public string Name { get; init; }
        public string PassportNumber { get; init; }
        public PassengerType PassengerType { get; init; }
        public int Age { get; init; }
    }

    public enum PassengerType
    {
        Male,
        Female,
        Baby,
        Unknown
    }

    public record UserCreated(long Id, string Name, string PassportNumber);
}
