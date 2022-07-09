using MassTransit;
using MediatR;
using NewBookingApp.Core.Contracts;

namespace NewBookingApp.Flight.API.Command.ResearveSeat
{
    public class ReserveSeatConsumer : IConsumer<ReserveSeatRequestDto>
    {
   
        private IMediator _mediator;

        public ReserveSeatConsumer( IMediator mediator)
        {
            _mediator = mediator;
         
        }

        public async Task Consume(ConsumeContext<ReserveSeatRequestDto> context)
        {
            var command = new ReserveSeatCommand(context.Message.FlightId, context.Message.SeatNumber);

           var result = await _mediator.Send(command);
        }
    }
}
