using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using NewBookingApp.Core.CQRS;
using NewBookingProject.Passenger.API.Data;
using NewBookingProject.Passenger.API.DTOs;

namespace NewBookingProject.Passenger.API.Queries.GetPassengerById
{
    public class GetPassengerQueryByIdHandler : IQueryHandler<GetPassengerQueryById, PassengerResponseDto>
    {
        private readonly PassengerDbContext _passengerDbContext;
        private readonly IMapper _mapper;

        public GetPassengerQueryByIdHandler(IMapper mapper, PassengerDbContext passengerDbContext)
        {
            _mapper = mapper;
            _passengerDbContext = passengerDbContext;
        }

        public async Task<PassengerResponseDto> Handle(GetPassengerQueryById query, CancellationToken cancellationToken)
        {
            var passenger =
                await _passengerDbContext.Passengers.SingleOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

            if (passenger is null)
                throw new NotImplementedException();

            return _mapper.Map<PassengerResponseDto>(passenger!);
        }
    }
}
