using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TryOut.NotificationPattern.Api.Commands.FluentValidation;
using TryOut.NotificationPattern.Api.Notifications.FluentValidation;
using TryOut.NotificationPattern.Api.Requests.Queries.FluentValidation;
using TryOut.NotificationPattern.Domain.Customers.FluentValidation;

namespace TryOut.NotificationPattern.Api.Handlers
{
    public class CreateCustomerValidatedWithFluentValidationHandler : IRequestHandler<CreateCustomerWithFluentValidationCommand, long?>,
                                                                      IRequestHandler<GetCustomerByIdWithFluentValidationQuery, string>
    {
        private readonly ICustomerRepositoryForFluentValidation _customerRepositoryForFluentValidation;
        private readonly INotificationContextForFluentValidation _notificationContextForFluentValidation;

        public CreateCustomerValidatedWithFluentValidationHandler(ICustomerRepositoryForFluentValidation customerRepositoryForFluentValidation,
            INotificationContextForFluentValidation notificationContextForFluentValidation)
        {
            _customerRepositoryForFluentValidation = customerRepositoryForFluentValidation;
            _notificationContextForFluentValidation = notificationContextForFluentValidation;
        }

        public async Task<long?> Handle(CreateCustomerWithFluentValidationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                {
                    _notificationContextForFluentValidation.AddNotification("Request Invalid", "The request command must be informed.");
                    return default;
                }

                var customer = new CustomerForFluentValidation(
                    request.Id,
                    request.Birth,
                    request.Document,
                    request.Name,
                    request.Credits);

                await _customerRepositoryForFluentValidation.SaveAsync(customer);

                if (!customer.Valid)
                {
                    _notificationContextForFluentValidation.AddNotifications(customer.ValidationResult);
                    return default;
                }

                return customer.Id;
            }
            catch (Exception ex)
            {
                _notificationContextForFluentValidation.AddNotification(ex.GetType().Name, ex.InnerException?.Message ?? ex.Message);
                return null;
            }
        }

        public async Task<string> Handle(GetCustomerByIdWithFluentValidationQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                {
                    _notificationContextForFluentValidation.AddNotification("Request Invalid", "The request query must be informed.");
                    return default;
                }

                var customer = await _customerRepositoryForFluentValidation.GetAsync(request.Id);
                if (customer is null)
                {
                    _notificationContextForFluentValidation.AddNotification(new Notification("404", "Customer can't be find."));
                    return default;
                }

                return customer.ToString();
            }
            catch (Exception ex)
            {
                _notificationContextForFluentValidation.AddNotification(ex.GetType().Name, ex.InnerException?.Message ?? ex.Message);
                return null;
            }
        }
    }
}