using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using TryOut.NotificationPattern.Api.Commands.FluentValidation;
using TryOut.NotificationPattern.Api.Controllers.v1.Abstractions;
using TryOut.NotificationPattern.Api.Requests.Queries.FluentValidation;

namespace TryOut.NotificationPattern.Api.Controllers.v1
{
    [Route("api/v{version:apiVersion}/customer")]
    public class CustomerController : ApiV1Controller
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("fluent-validation")]
        public async Task<IActionResult> GetAsync([FromQuery] GetCustomerByIdWithFluentValidationQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("fluent-validation")]
        public async Task<IActionResult> PostAsync([FromBody] CreateCustomerWithFluentValidationCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok($"Customer saved! Id: {result}");
        }
    }
}