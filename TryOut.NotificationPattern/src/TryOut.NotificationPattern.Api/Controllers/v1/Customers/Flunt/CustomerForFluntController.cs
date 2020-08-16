using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using TryOut.NotificationPattern.Api.Commands.Flunt;
using TryOut.NotificationPattern.Api.Controllers.v1.Abstractions;
using TryOut.NotificationPattern.Api.Requests.Queries.Flunt;

namespace TryOut.NotificationPattern.Api.Controllers.v1.Customers.Flunt
{
    [Route("api/v{version:apiVersion}/customer/flunt")]
    public class CustomerForFluntController : ApiV1Controller
    {
        private readonly IMediator _mediator;

        public CustomerForFluntController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Finds a Customer.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/customer/fluent-validation
        ///     {
        ///        "id": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A string that describe the Customer selected.</returns>
        /// <response code="200">If the Customer has been find.</response>
        /// <response code="400">If the validation failed or the Customer doesn't exist in context.</response>
        [HttpGet]
        [Produces("text/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync([FromQuery] GetCustomerByIdWithFluntQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Creates a Customer.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/v1/customer/flunt
        ///     {
        ///         "birth": "1990-08-16T21:51:33.252Z",
        ///         "credits": 100,
        ///         "document": "012345678901",
        ///         "id": 1,
        ///         "name": "Carlos Dias"
        ///     }
        /// </remarks>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A ID of the Customer created.</returns>
        /// <response code="200">Returns the new created item.</response>
        /// <response code="400">If the validation failed.</response>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync([FromBody] CreateCustomerWithFluntCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
    }
}