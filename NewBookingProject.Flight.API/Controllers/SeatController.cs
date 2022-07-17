using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewBookingApp.Flight.API.Command.ResearveSeat;
using NewBookingApp.Flight.API.Queries.GetAvailableSeats;

namespace NewBookingApp.Flight.API.Controllers
{
    [Route("api/assentos")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SeatController(IMediator mediator)
        {
            _mediator = mediator;
        }

       // [Authorize]
        [HttpGet("{FlightId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetAvailableSeats([FromRoute] GetAvailableSeatsQuery query,
      CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

       // [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ReserveSeat([FromBody] ReserveSeatCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }
    }
}
