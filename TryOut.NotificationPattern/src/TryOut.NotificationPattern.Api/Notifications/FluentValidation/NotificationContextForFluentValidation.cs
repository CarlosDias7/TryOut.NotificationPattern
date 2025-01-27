﻿using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace TryOut.NotificationPattern.Api.Notifications.FluentValidation
{
    public class NotificationContextForFluentValidation : INotificationContextForFluentValidation
    {
        private readonly List<Notification> _notifications;

        public NotificationContextForFluentValidation()
        {
            _notifications = new List<Notification>();
        }

        public bool HasNotifications => _notifications?.Any() ?? false;
        public IReadOnlyCollection<Notification> Notifications => _notifications;

        public void AddNotification(string key, string message)
        {
            _notifications.Add(new Notification(key, message));
        }

        public void AddNotification(Notification notification)
        {
            _notifications.Add(notification);
        }

        public void AddNotifications(IEnumerable<Notification> notifications)
        {
            _notifications.AddRange(notifications);
        }

        public void AddNotifications(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                AddNotification(error.PropertyName, error.ErrorMessage);
            }
        }
    }
}