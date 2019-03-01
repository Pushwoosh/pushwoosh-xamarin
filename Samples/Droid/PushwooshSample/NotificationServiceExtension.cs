using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Pushwoosh.Notification;
using Org.Json;
using Android.Support.Annotation;
using Android.OS;

[assembly: MetaData("com.pushwoosh.notification_service_extension",
                    Value = "com.pushwoosh.test.NotificationServiceExtension")]
namespace PushwooshSample
{
    [Register("com/pushwoosh/test/NotificationServiceExtension")]
    public class NotificationServiceExtension : Pushwoosh.Notification.NotificationServiceExtension
    {
        public static Activity CurrentActivity { get; set; }
        protected override bool OnMessageReceived(PushMessage message)
        {
            if (IsAppOnForeground)
            {
                Handler mainHandler = new Handler(ApplicationContext.MainLooper);
                mainHandler.Post(() =>
                {
                    HandlePush(message);
                });
            }
            return false;
        }

        protected override void StartActivityForPushMessage(PushMessage message)
        {
            if (!HandlePush(message))
            {
                base.StartActivityForPushMessage(message);
            }
        }

        [MainThread]
        bool HandlePush(PushMessage message)
        {
            try
            {
                Intent intent = new Intent(ApplicationContext, typeof(SecondActivity));
                intent.SetFlags(ActivityFlags.NewTask);
                intent.PutExtra(SecondActivity.PUSH_MESSAGE_KEY, message.ToJson().ToString());
                ApplicationContext.StartActivity(intent);
                return true;
            }
            catch (Exception)
            {}
            return false;
        }
    }
}
