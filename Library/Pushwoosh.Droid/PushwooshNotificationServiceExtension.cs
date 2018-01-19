using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Runtime;

[assembly: MetaData("com.pushwoosh.notification_service_extension",
                    Value = "com.pushwoosh.plugin.PushwooshNotificationServiceExtension")]
namespace Pushwoosh.Droid
{
    [Register("com/pushwoosh/plugin/PushwooshNotificationServiceExtension")]
    public class PushwooshNotificationServiceExtension : Notification.NotificationServiceExtension
    {
        List<Notification.PushMessage> rememberedReceivedPushes = new List<Notification.PushMessage>();
        List<Notification.PushMessage> rememberedOpenedPushes = new List<Notification.PushMessage>();
        protected override bool OnMessageReceived(Notification.PushMessage pushMessage)
        {
            base.OnMessageReceived(pushMessage);
            if (PushManager.Instance == null)
            {
                rememberedReceivedPushes.Add(pushMessage);
                PushManager.InitFinished += OnPushwooshInitialized;
                return false;
            }
            else
            {
                return CallPushReceived(pushMessage);
            }
        }

        bool CallPushReceived(Notification.PushMessage pushMessage) {
            new Handler(ApplicationContext.MainLooper).Post(() =>
            {
                PushManager.Instance.OnPushReceived(PushManager.NotificationFromNative(pushMessage), !IsAppOnForeground);
            });
            return IsAppOnForeground && !PushManager.Instance.ShowPushNotificationAlerts;
        }

        protected override void OnMessageOpened(Notification.PushMessage pushMessage)
        {
            base.OnMessageOpened(pushMessage);
            if (PushManager.Instance == null)
            {
                rememberedOpenedPushes.Add(pushMessage);
                PushManager.InitFinished += OnPushwooshInitialized;
            }
            else
            {
                CallPushAccepted(pushMessage);
            }
        }

        void CallPushAccepted(Notification.PushMessage pushMessage) {
            new Handler(ApplicationContext.MainLooper).Post(() =>
            {
                PushManager.Instance.OnPushAccepted(PushManager.NotificationFromNative(pushMessage), !IsAppOnForeground);
            });
        }

        void OnPushwooshInitialized(object sender, EventArgs args) {
            if (PushManager.Instance == null)
                return;
            foreach (Notification.PushMessage message in rememberedReceivedPushes)
            {
                CallPushReceived(message);
            }
            rememberedReceivedPushes.Clear();
            foreach (Notification.PushMessage message in rememberedOpenedPushes)
            {
                CallPushAccepted(message);
            }
            rememberedOpenedPushes.Clear();
        }
    }
}