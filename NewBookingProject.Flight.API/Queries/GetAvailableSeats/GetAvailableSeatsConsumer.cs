using MapsterMapper;
using MassTransit;
using MediatR;
using NewBookingApp.Core.Contracts;
using NewBookingApp.Flight.Infra.Context;

namespace NewBookingApp.Flight.API.Queries.GetAvailableSeats
{
    public class GetAvailableSeatsConsumer : IConsumer<GetAvailabeSeatsbyId>
    {
        private readonly IMapper _mapper;
        private FlightDbContext _context;
        private IMediator _mediator;

        public GetAvailableSeatsConsumer(FlightDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task Consume(ConsumeContext<GetAvailabeSeatsbyId> context)
        {
            var flighId = context.Message.FlightId;
            var query = new GetAvailableSeatsQuery(flighId);
            var seatList = await _mediator.Send(query);

            var message = seatList.First(); // pega o primeiro assento vazio

            await context.RespondAsync<SeatResponse>(message);
        }
    }
}
