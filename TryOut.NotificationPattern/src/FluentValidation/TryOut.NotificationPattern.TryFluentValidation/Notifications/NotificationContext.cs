﻿using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace TryOut.NotificationPattern.TryFluentValidation.Notifications
{
    public class NotificationContext
    {
        private readonly List<Notification> _notifications;

        public NotificationContext()
        {
            _notifications = new List<Notification>();
        }

        public bool HasNotifications => _notifications?.Any() ?? false;
        public IReadOnlyCollection<Notification> Notifications => _notifications;

        public void AddNotification(string key, string message) => _notifications.Add(new Notification(key, message));

        public void AddNotification(Notification notification) => _notifications.Add(notification);

        public void AddNotifications(IEnumerable<Notification> notifications) => _notifications.AddRange(notifications);

        public void AddNotifications(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                AddNotification(error.ErrorCode, error.ErrorMessage);
            }
        }
    }
}