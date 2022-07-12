using FluentValidation;
using NewBookingApp.Flight.API.Queries.GetAvailableSeats;

namespace NewBookingApp.Flight.API.Application.Queries.GetAvailableSeats
{
    public class GetAvailableSeatsQueryValidator : AbstractValidator<GetAvailableSeatsQuery>
    {
        public GetAvailableSeatsQueryValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.FlightId).NotNull().WithMessage("FlightId is required!");
        }
    }
}

