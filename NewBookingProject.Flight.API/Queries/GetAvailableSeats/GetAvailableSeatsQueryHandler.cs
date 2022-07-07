using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using NewBookingApp.Core.CQRS;
using NewBookingApp.Flight.API.Dtos;
using NewBookingApp.Flight.Infra.Context;

namespace NewBookingApp.Flight.API.Queries.GetAvailableSeats
{
    public class GetAvailableSeatsQueryHandler : IQueryHandler<GetAvailableSeatsQuery, IEnumerable<SeatResponseDto>>
    {
        private readonly FlightDbContext _flightDbContext;
        private readonly IMapper _mapper;

        public GetAvailableSeatsQueryHandler(IMapper mapper, FlightDbContext flightDbContext)
        {
            _mapper = mapper;
            _flightDbContext = flightDbContext;
        }


        public async Task<IEnumerable<SeatResponseDto>> Handle(GetAvailableSeatsQuery query, CancellationToken cancellationToken)
        {

            var seats = await _flightDbContext.Seats.Where(x => x.FlightId == query.FlightId && !x.IsDeleted).ToListAsync(cancellationToken);

            if (!seats.Any())
                throw new NotImplementedException();

            return _mapper.Map<List<SeatResponseDto>>(seats);
        }
    }
}
