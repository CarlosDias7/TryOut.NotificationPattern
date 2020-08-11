using FluentValidation.Results;
using System.Collections.Generic;

namespace TryOut.NotificationPattern.Api.Notifications.FluentValidation
{
    public interface INotificationContextForFluentValidation
    {
        bool HasNotifications { get; }

        IReadOnlyCollection<Notification> Notifications { get; }

        void AddNotification(string key, string message);

        void AddNotification(Notification notification);

        void AddNotifications(IEnumerable<Notification> notifications);

        void AddNotifications(ValidationResult validationResult);
    }
}