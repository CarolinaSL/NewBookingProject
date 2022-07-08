using NewBookingApp.Core.CQRS;
using NewBookingApp.Core.Generators;
using NewBookingProject.Passenger.API.DTOs;
using NewBookingProject.Passenger.API.Passengers.Models;

namespace NewBookingProject.Passenger.API.Commands.CompleteRegisterPassenger
{
    public record CompleteRegisterPassengerCommand(string PassportNumber, PassengerType PassengerType, int Age) : ICommand<PassengerResponseDto>
    {
        public long Id { get; set; } = SnowFlakIdGenerator.NewId();
    }
}
