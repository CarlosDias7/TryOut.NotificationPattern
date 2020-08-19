using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using TryOut.NotificationPattern.Api.Commands.FluentValidation;
using TryOut.NotificationPattern.Api.Controllers.v1.Abstractions;
using TryOut.NotificationPattern.Api.Requests.Commands.FluentValidation;
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

        /// <summary>
        /// Deletes a Customer.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/v1/customer/fluent-validation
        ///     {
        ///        "id": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A string that describe the result of the action.</returns>
        /// <response code="200">If the Customer has been deleted.</response>
        /// <response code="400">If the validation failed or the Customer doesn't exist in context.</response>
        [HttpDelete]
        [Produces("text/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAsync([FromBody] DeleteCustomerWithFluentValidationCommand command, CancellationToken cancellationToken)
            => await _mediator.Send(command, cancellationToken)
                ? Ok("Customer deleted!")
                : (IActionResult)BadRequest("Can't delete the Customer!");

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
        public async Task<IActionResult> GetAsync([FromQuery] GetCustomerByIdWithFluentValidationQuery query, CancellationToken cancellationToken)
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
        ///     POST /api/v1/customer/fluent-validation
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
        /// <response code="200">Returns the newly created item.</response>
        /// <response code="400">If the validation failed or the Customer has already exist in context.</response>
        [HttpPost]
        [Produces("text/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync([FromBody] CreateCustomerWithFluentValidationCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok($"Customer created! ID: {result}.");
        }
    }
}