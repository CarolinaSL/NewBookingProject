using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewBookingApp.Booking.API.Command.CreateBooking;

namespace NewBookingApp.Booking.API.Controllers
{

    [ApiController]
    [Route("api/reservas")]
    public class BookingController : ControllerBase
    {

        private readonly IMediator _mediator;

        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
      //  [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
       // [SwaggerOperation(Summary = "Create new Reservation", Description = "Create new Reservation")]
        public async Task<ActionResult> CreateReservation([FromBody] CreateBookingCommand command,
       CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }
    }

}
