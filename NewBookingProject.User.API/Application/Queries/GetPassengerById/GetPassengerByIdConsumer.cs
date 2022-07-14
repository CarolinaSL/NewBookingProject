using Mapster;
using MassTransit;
using MediatR;
using NewBookingApp.Core.Contracts;

namespace NewBookingProject.Passenger.API.Queries.GetPassengerById
{
    public class GetPassengerByIdConsumer : IConsumer<GetPassengerByIdRequest>
    {

        private IMediator _mediator;

        public GetPassengerByIdConsumer(IMediator mediator)
        {
            _mediator = mediator;

        }

        public async Task Consume(ConsumeContext<GetPassengerByIdRequest> context)
        {
            var query = new GetPassengerQueryById(context.Message.PassengerId);

            var passengerResponseDto =await _mediator.Send(query);

            var result = passengerResponseDto.Adapt<PassengerResponse>();

            await context.RespondAsync(result);

        }
    }
}
