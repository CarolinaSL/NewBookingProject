using FluentValidation;
using NewBookingApp.Booking.API.Command.CreateBooking;

namespace NewBookingApp.Booking.API.Application.Command.CreateBooking
{
    public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingCommandValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.FlightId).NotNull().WithMessage("FlightId é obrigatório!");
            RuleFor(x => x.PassengerId).NotNull().WithMessage("PassengerId é obrigatório!");
        }
    }
}
