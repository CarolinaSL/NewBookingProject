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

        public async Task Consume(ConsumeContext<ReserveSeatRequestDto> command)
        {

           await _mediator.Send(command);
        }
    }
}
