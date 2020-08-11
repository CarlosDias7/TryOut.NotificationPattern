using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using TryOut.NotificationPattern.Api.Notifications.FluentValidation;

namespace TryOut.NotificationPattern.Api.Filters
{
    public class NotificationFilter : IAsyncResultFilter
    {
        private const string ContentTypeJson = "application/json";
        private readonly INotificationContextForFluentValidation _notificationContextForFluentValidation;

        public NotificationFilter(INotificationContextForFluentValidation notificationContextForFluentValidation)
        {
            _notificationContextForFluentValidation = notificationContextForFluentValidation;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (_notificationContextForFluentValidation.HasNotifications)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.HttpContext.Response.ContentType = ContentTypeJson;

                var notifications = JsonConvert.SerializeObject(_notificationContextForFluentValidation.Notifications);
                await context.HttpContext.Response.WriteAsync(notifications);

                return;
            }

            await next();
        }
    }
}