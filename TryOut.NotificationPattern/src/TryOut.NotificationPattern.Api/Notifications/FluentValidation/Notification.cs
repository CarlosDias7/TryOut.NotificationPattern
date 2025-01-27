﻿namespace TryOut.NotificationPattern.Api.Notifications.FluentValidation
{
    public class Notification
    {
        public Notification(string property, string message)
        {
            Property = property;
            Message = message;
        }

        public string Message { get; }
        public string Property { get; }
    }
}