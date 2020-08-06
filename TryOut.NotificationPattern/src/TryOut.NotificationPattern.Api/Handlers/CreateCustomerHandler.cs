using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TryOut.NotificationPattern.Api.Commands.FluentValidation;
using TryOut.NotificationPattern.Domain.FluentValidation;

namespace TryOut.NotificationPattern.Api.Handlers
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerWithFluentValidationCommand, long>
    {
        private readonly ICustomerRepository _customerRepositoryForFluentValidation;

        public CreateCustomerHandler(ICustomerRepository customerRepositoryForFluentValidation)
        {
            _customerRepositoryForFluentValidation = customerRepositoryForFluentValidation;
        }

        public async Task<long> Handle(CreateCustomerWithFluentValidationCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}