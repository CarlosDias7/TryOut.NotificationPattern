using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using TryOut.NotificationPattern.Api.Notifications.FluentValidation;
using TryOut.NotificationPattern.Api.Notifications.Flunt;

namespace TryOut.NotificationPattern.Api.Filters
{
    public class NotificationFilter : IAsyncResultFilter
    {
        private const string ContentTypeJson = "application/json";
        private readonly INotificationContextForFluentValidation _notificationContextForFluentValidation;
        private readonly INotificationContextForFlunt _notificationContextForFlunt;

        public NotificationFilter(INotificationContextForFluentValidation notificationContextForFluentValidation,
            INotificationContextForFlunt notificationContextForFlunt)
        {
            _notificationContextForFluentValidation = notificationContextForFluentValidation;
            _notificationContextForFlunt = notificationContextForFlunt;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (await CheckForFluentValidationNotificationsAsync(context)) return;
            if (await CheckForFluntNotifications(context)) return;

            await next();
        }

        private async Task<bool> CheckForFluentValidationNotificationsAsync(ResultExecutingContext context)
        {
            if (!_notificationContextForFluentValidation.HasNotifications) return false;

            SetBadRequest(context);
            var notificationsByFluentValidation = JsonConvert.SerializeObject(_notificationContextForFluentValidation.Notifications);
            await context.HttpContext.Response.WriteAsync(notificationsByFluentValidation);

            return true;
        }

        private async Task<bool> CheckForFluntNotifications(ResultExecutingContext context)
        {
            if (_notificationContextForFlunt.Valid) return false;

            SetBadRequest(context);
            var notificationsByFlunt = JsonConvert.SerializeObject(_notificationContextForFlunt.Notifications);
            await context.HttpContext.Response.WriteAsync(notificationsByFlunt);

            return true;
        }

        private void SetBadRequest(ResultExecutingContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.HttpContext.Response.ContentType = ContentTypeJson;
        }
    }
}