using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TryOut.NotificationPattern.Api.Commands.Flunt;
using TryOut.NotificationPattern.Api.Notifications.Flunt;
using TryOut.NotificationPattern.Api.Requests.Queries.Flunt;
using TryOut.NotificationPattern.Domain.Customers.Flunt;

namespace TryOut.NotificationPattern.Api.Requests.Handlers
{
    public class CustomerValidatedWithFluntHandler : IRequestHandler<CreateCustomerWithFluntCommand, long?>,
                                                     IRequestHandler<GetCustomerByIdWithFluntQuery, string>
    {
        private readonly ICustomerRepositoryForFlunt _customerRepositoryForFlunt;
        private readonly INotificationContextForFlunt _notificationContextForFlunt;

        public CustomerValidatedWithFluntHandler(ICustomerRepositoryForFlunt customerRepositoryForFlunt,
            INotificationContextForFlunt notificationContextForFlunt)
        {
            _customerRepositoryForFlunt = customerRepositoryForFlunt;
            _notificationContextForFlunt = notificationContextForFlunt;
        }

        public async Task<long?> Handle(CreateCustomerWithFluntCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                {
                    _notificationContextForFlunt.AddNotification("Request Invalid", "The request command must be informed.");
                    return default;
                }

                var customer = new CustomerForFlunt(
                    request.Id,
                    request.Birth,
                    request.Document,
                    request.Name,
                    request.Credits);

                await _customerRepositoryForFlunt.SaveAsync(customer);

                if (!customer.Valid)
                {
                    _notificationContextForFlunt.AddNotifications(customer.Notifications);
                    return default;
                }

                return customer.Id;
            }
            catch (Exception ex)
            {
                _notificationContextForFlunt.AddNotification(ex.GetType().Name, ex.InnerException?.Message ?? ex.Message);
                return null;
            }
        }

        public async Task<string> Handle(GetCustomerByIdWithFluntQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                {
                    _notificationContextForFlunt.AddNotification("Request Invalid", "The request query must be informed.");
                    return default;
                }

                var customer = await _customerRepositoryForFlunt.GetAsync(request.Id);
                if (customer is null)
                {
                    _notificationContextForFlunt.AddNotification("404", "Customer can't be find.");
                    return default;
                }

                return customer.ToString();
            }
            catch (Exception ex)
            {
                _notificationContextForFlunt.AddNotification(ex.GetType().Name, ex.InnerException?.Message ?? ex.Message);
                return null;
            }
        }
    }
}