using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewBookingProject.Passenger.API.Commands.CompleteRegisterPassenger;
using NewBookingProject.Passenger.API.Queries.GetPassengerById;

namespace NewBookingProject.Passenger.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PassengerController(IMediator mediator)
        {
            _mediator = mediator;
        }


        //  [Authorize]
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
      //  [SwaggerOperation(Summary = "Get passenger by id", Description = "Get passenger by id")]
        public async Task<ActionResult> GetById([FromRoute] GetPassengerQueryById query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        //[Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[SwaggerOperation(Summary = "Complete Register Passenger", Description = "Complete Register Passenger")]
        public async Task<ActionResult> CompleteRegisterPassenger([FromBody] CompleteRegisterPassengerCommand command,
       CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }
    }
}
