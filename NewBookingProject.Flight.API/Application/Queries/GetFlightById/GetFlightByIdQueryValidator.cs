using FluentValidation;
using NewBookingApp.Flight.API.Queries.GetFlightById;

namespace NewBookingApp.Flight.API.Application.Queries.GetFlightById
{
    public class GetFlightByIdQueryValidator : AbstractValidator<GetFlightByIdQuery>
    {
        public GetFlightByIdQueryValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Id).NotNull().WithMessage("Id is required!");
        }
    }
}
