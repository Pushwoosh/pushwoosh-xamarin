using System;
namespace Pushwoosh
{
    public class PushNotificationEventArgs : EventArgs
    {
        public PushNotification Notification { get; set; }
        public bool FromBackground { get; set; }
    }
}
