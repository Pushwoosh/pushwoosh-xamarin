using System;

namespace Pushwoosh.iOS
{
    public class PushNotificationSettings : global::Pushwoosh.PushNotificationSettings
    {
        public bool Alert { get; internal set; }
        public bool Badge { get; internal set; }
        public bool Sound { get; internal set; }
    }
}
