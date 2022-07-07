using MediatR;
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
        private readonly ILogger<SeatController> _logger;
        private readonly IMediator _mediator;

        public SeatController(ILogger<SeatController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        //[Authorize]
        [HttpGet("{FlightId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
     //   [SwaggerOperation(Summary = "Get available seats", Description = "Get available seats")]
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
     //   [SwaggerOperation(Summary = "Reserve seat", Description = "Reserve seat")]
        public async Task<ActionResult> ReserveSeat([FromBody] ReserveSeatCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }
    }
}
