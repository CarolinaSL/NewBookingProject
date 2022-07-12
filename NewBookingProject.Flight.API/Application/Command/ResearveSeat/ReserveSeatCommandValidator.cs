using FluentValidation;
using NewBookingApp.Flight.API.Command.ResearveSeat;

namespace NewBookingApp.Flight.API.Application.Command.ResearveSeat
{
    public class ReserveSeatCommandValidator : AbstractValidator<ReserveSeatCommand>
    {
        public ReserveSeatCommandValidator()
        {
            // para de executar o validador assim que uma regra falha
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.FlightId).NotEmpty().WithMessage("FlightId não deve ser vazio!");
            RuleFor(x => x.SeatNumber).NotEmpty().WithMessage("SeatNumber não deve ser vazio!");
        }
    }
}
