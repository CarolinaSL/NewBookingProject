using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewBookingApp.Flight.API.Queries.GetAvailableFlights;
using NewBookingApp.Flight.API.Queries.GetFlightById;

namespace NewBookingProject.Flight.API.Controllers
{
    [ApiController]
    [Route("api/voos")]
    public class FlightController : ControllerBase
    {

        private readonly IMediator _mediator;

        public FlightController(IMediator mediator)
        {
            
            _mediator = mediator;
        }

        //[Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetAvailableFlights([FromRoute] GetAvailableFlightsQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }


       // [Authorize]
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetById([FromRoute] GetFlightByIdQuery query, CancellationToken cancellationToken)
        {
          
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }
    }

}