using Flunt.Notifications;
using System.Collections.Generic;

namespace TryOut.NotificationPattern.Api.Notifications.Flunt
{
    public interface INotificationContextForFlunt
    {
        IReadOnlyCollection<Notification> Notifications { get; }
        bool Valid { get; }

        void AddNotification(string property, string message);

        void AddNotification(Notification notification);

        void AddNotifications(IReadOnlyCollection<Notification> notifications);

        void AddNotifications(IList<Notification> notifications);

        void AddNotifications(ICollection<Notification> notifications);

        void AddNotifications(Notifiable item);

        void AddNotifications(params Notifiable[] items);
    }
}