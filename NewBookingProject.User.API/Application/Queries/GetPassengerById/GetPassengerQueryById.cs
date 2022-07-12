using NewBookingApp.Core.CQRS;
using NewBookingProject.Passenger.API.DTOs;

namespace NewBookingProject.Passenger.API.Queries.GetPassengerById
{
    public record GetPassengerQueryById(long Id) : IQuery<PassengerResponseDto>;
}
