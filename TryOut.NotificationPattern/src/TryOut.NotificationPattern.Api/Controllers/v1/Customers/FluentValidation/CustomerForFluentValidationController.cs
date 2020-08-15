using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using TryOut.NotificationPattern.Api.Commands.FluentValidation;
using TryOut.NotificationPattern.Api.Controllers.v1.Abstractions;
using TryOut.NotificationPattern.Api.Requests.Queries.FluentValidation;

namespace TryOut.NotificationPattern.Api.Controllers.v1.Customers
{
    [Route("api/v{version:apiVersion}/customer/fluent-validation")]
    public class CustomerForFluentValidationController : ApiV1Controller
    {
        private readonly IMediator _mediator;

        public CustomerForFluentValidationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] GetCustomerByIdWithFluentValidationQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Creates a Customer.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>A ID of the Customer created.</returns>
        /// <response code="201">Returns the newly created item.</response>
        /// <response code="400">If the validation failed.</response>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync([FromBody] CreateCustomerWithFluentValidationCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
    }
}