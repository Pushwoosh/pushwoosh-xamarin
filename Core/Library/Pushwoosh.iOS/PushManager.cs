using System;
using System.Threading.Tasks;
using Pushwoosh;
using Foundation;
using UIKit;
using System.Diagnostics.Contracts;
using UserNotifications;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;

namespace Pushwoosh.iOS
{
    public class PushManager : Pushwoosh.PushManager
    {
        public static new PushManager Instance => global::Pushwoosh.PushManager.Instance as PushManager;
        const string PWMLocalNotificationUidKey = "pw_localnotification_uid";
        int LocalNotificationUid = 0;
        public static PushwooshError ErrorFromNSError(NSError error)
        {
            return error != null ? new PushwooshError { NativeError = error, Description = error.LocalizedDescription } : null;
        }
        public static PushNotification PushNotificationFromUserInfo(NSDictionary pushNotification)
        {
            NSDictionary apnPayload = Instance.nativeManager.GetApnPayload(pushNotification);
            bool alertIsDict = apnPayload["alert"] is NSDictionary;
            NSObject messageObject = null;
            NSObject titleObject = null;
            if (alertIsDict)
            {
                titleObject = (apnPayload["alert"] as NSDictionary)["title"];
                messageObject = (apnPayload["alert"] as NSDictionary)["body"];
            }
            else
            {
                messageObject = apnPayload["alert"];
            }
            NSError error = null;
            string pushPayloadString = new NSString(NSJsonSerialization.Serialize(pushNotification, 0, out error), NSStringEncoding.UTF8);
            return new PushNotification
            {
                Message = messageObject != null ? messageObject.ToString() : null,
                Title = titleObject != null ? titleObject.ToString() : null,
                CustomData = Instance.nativeManager.GetCustomPushData(pushNotification),
                Payload = pushPayloadString
            };
        }
        class PushwooshDelegate : PushNotificationDelegate
        {
            // Methods
            //
            public override void OnDidFailToRegisterForRemoteNotificationsWithError(NSError error)
            {
                Instance.OnRegisterError(ErrorFromNSError(error));
            }
            public override void OnDidRegisterForRemoteNotificationsWithDeviceToken(NSString token)
            {
                Instance.OnRegistered();
            }
            public override void OnPushAccepted(PushNotificationManager pushManager, NSDictionary pushNotification, bool onStart)
            {
                Instance.OnPushAccepted(PushNotificationFromUserInfo(pushNotification), onStart);
            }
            public override void OnPushReceived(PushNotificationManager pushManager, NSDictionary pushNotification, bool onStart)
            {
                Instance.OnPushReceived(PushNotificationFromUserInfo(pushNotification), onStart);
            }
        }
        PushNotificationManager nativeManager;
        PushwooshDelegate pushwooshDelegate;

        public PushManager()
        {
            nativeManager = PushNotificationManager.PushManager;
            pushwooshDelegate = new PushwooshDelegate();
            nativeManager.Delegate = pushwooshDelegate;
            if (UNUserNotificationCenter.Current != null)
                UNUserNotificationCenter.Current.Delegate = nativeManager.NotificationCenterDelegate;
            InAppManager = new InAppManager(PWInAppManager.SharedManager);
            nativeManager.SendAppOpen();
        }

        public override string AppCode => nativeManager.AppCode;

        public override string HardwareId => nativeManager.HWID;

        public override string PushToken => nativeManager.PushToken;

        public override Task<RequestResult<TagsBundle>> LoadTagsAsync()
        {
            var source = new TaskCompletionSource<RequestResult<TagsBundle>>();
            nativeManager.LoadTags((NSDictionary tagsDict) =>
            {
                var tags = new TagsBundle();
                foreach (var kv in tagsDict)
                {
                    if (kv.Key is NSString)
                    {
                        if (kv.Value is NSString)
                            tags.PutString(kv.Key.ToString(), kv.Value.ToString());
                        else if (kv.Value is NSNumber)
                            tags.PutInteger(kv.Key.ToString(), (kv.Value as NSNumber).Int32Value);
                        else if (kv.Value is NSArray)
                        {
                            tags.PutList(kv.Key.ToString(), NSArray.ArrayFromHandle<string>(kv.Value.Handle, (input) => new NSObject(input).Description));
                        }
                    }
                }
                source.SetResult(new RequestResult<TagsBundle>() { Result = tags });
            }, (error) =>
            {
                source.SetResult(new RequestResult<TagsBundle>() { Error = ErrorFromNSError(error) });
            });
            return source.Task;
        }

        public override void Register()
        {
            nativeManager.RegisterForPushNotifications();
        }

        public override void SendPurchaseData(string identifier, decimal price, string currency)
        {
            nativeManager.SendPurchase(new NSString(identifier), new NSDecimalNumber(price.ToString(CultureInfo.InvariantCulture)), new NSString(currency), NSDate.Now);
        }

        public override Task<RequestResult> SendTagsAsync(TagsBundle tags)
        {
            if (tags == null)
                return null;
            var source = new TaskCompletionSource<RequestResult>();
            var dict = tags.ToDictionary;
            NSMutableDictionary tagsDict = new NSMutableDictionary();
            foreach (var kv in dict)
            {
                if (kv.Value is Nullable<int>)
                    tagsDict[kv.Key] = NSNumber.FromInt32((kv.Value as Nullable<int>).Value);
                else if (kv.Value is string)
                    tagsDict[kv.Key] = new NSString(kv.Value as string);
                else if (kv.Value is TagsBundle.IncrementalInteger)
                    tagsDict[kv.Key] = PWTags.IncrementalTagWithInteger((kv.Value as TagsBundle.IncrementalInteger).Value);
                else if (kv.Value is IList<string>)
                    tagsDict[kv.Key] = NSArray.FromNSObjects<string>((string arg) => new NSString(arg), (kv.Value as IList<string>).ToArray());
            }
            nativeManager.SetTags(tagsDict, (NSError error) =>
            {
                source.SetResult(new RequestResult { Error = ErrorFromNSError(error) });
            });
            return source.Task;
        }

        public override void Unregister()
        {
            nativeManager.UnregisterForPushNotifications();
        }

        public void RegisteredForRemoteNotifications(NSData deviceToken)
        {
            nativeManager.HandlePushRegistration(deviceToken);
        }

        public void FailedToRegisterForRemoteNotifications(NSError error)
        {
            nativeManager.HandlePushRegistrationFailure(error);
        }

        public void ReceivedRemoteNotification(NSDictionary userInfo)
        {
            nativeManager.HandlePushReceived(userInfo);
        }

        public override bool ShowPushNotificationAlerts
        {
            get
            {
                return nativeManager.ShowPushnotificationAlert;
            }
            set
            {
                nativeManager.ShowPushnotificationAlert = value;
            }
        }

        public new PushNotificationSettings NotificationSettings
        {
            get
            {
                NSDictionary status = PushNotificationManager.RemoteNotificationStatus;
                PushNotificationSettings notificationSettings = new PushNotificationSettings();
                notificationSettings.Enabled = (status["enabled"] as NSString) != "0";
                notificationSettings.Alert = (status["pushAlert"] as NSString) != "0";
                notificationSettings.Badge = (status["pushBadge"] as NSString) != "0";
                notificationSettings.Sound = (status["pushSound"] as NSString) != "0";
                return notificationSettings;
            }
        }

        string StringIdentifierFromInt(int identifier) {
            return PWMLocalNotificationUidKey + identifier;
        }

        public override int ScheduleLocalNotification(string body, int delay) {
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                UNUserNotificationCenter center = UNUserNotificationCenter.Current;
                UNMutableNotificationContent content = new UNMutableNotificationContent();
                content.Body = body;
                content.Sound = UNNotificationSound.Default;
                UNTimeIntervalNotificationTrigger trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(delay, false);
                UNNotificationRequest request = UNNotificationRequest.FromIdentifier(StringIdentifierFromInt(LocalNotificationUid), content, trigger);
                center.AddNotificationRequest(request, (NSError error) =>
                {
                    if (error != null)
                        Console.WriteLine("Something went wrong:" + error);
                });
            }
            else
            {
                UILocalNotification localNotification = new UILocalNotification();
                localNotification.FireDate = NSDate.FromTimeIntervalSinceNow(delay);
                localNotification.AlertBody = body;
                localNotification.TimeZone = NSTimeZone.DefaultTimeZone;
                localNotification.UserInfo = NSDictionary.FromObjectAndKey(NSNumber.FromInt32(LocalNotificationUid), new NSString(PWMLocalNotificationUidKey));
                UIApplication.SharedApplication.ScheduleLocalNotification(localNotification);
            }
            return LocalNotificationUid++;
        }

        public override void CancelLocalNotification(int notificationId)
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                UNUserNotificationCenter.Current.RemovePendingNotificationRequests(new[] { StringIdentifierFromInt(notificationId) });
            }
            else
            {
                UILocalNotification notificationToDelete = null;
                NSString uidKey = new NSString(PWMLocalNotificationUidKey);
                foreach (UILocalNotification notification in UIApplication.SharedApplication.ScheduledLocalNotifications)
                {
                    if (notification.UserInfo.ObjectForKey(uidKey).IsEqual(NSNumber.FromInt32(notificationId)))
                    {
                        notificationToDelete = notification;
                        break;
                    }
                }
                if (notificationToDelete != null)
                {
                    UIApplication.SharedApplication.ScheduledLocalNotifications = UIApplication.SharedApplication.ScheduledLocalNotifications.Where(n => n != notificationToDelete).ToArray();
                }
            }
        }

        public override void CancelAllLocalNotifications() {
            UIApplication.SharedApplication.ScheduledLocalNotifications = new UILocalNotification[] {};
        }

        protected override global::Pushwoosh.PushNotificationSettings GetNotificationSettings()
        {
            return NotificationSettings;
        }

        public PushNotification LaunchNotification => PushNotificationFromUserInfo(nativeManager.LaunchNotification);

        public static void Init() 
        {
            global::Pushwoosh.PushManager.Instance = new PushManager();
        }
    }
}
