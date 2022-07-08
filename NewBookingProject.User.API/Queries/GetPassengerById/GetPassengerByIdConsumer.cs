using MassTransit;
using MediatR;


namespace NewBookingProject.Passenger.API.Queries
{
    public class GetPassengerByIdConsumer : IConsumer<GetPassengerById>
    {

        private IMediator _mediator;

        public GetPassengerByIdConsumer(IMediator mediator)
        {
            _mediator = mediator;

        }

        
    }
}
