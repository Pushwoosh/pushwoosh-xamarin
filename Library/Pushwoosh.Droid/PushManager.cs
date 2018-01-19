using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pushwoosh;
using Pushwoosh.Function;
using Android.Runtime;
using System.Globalization;
using System.Linq;
using Pushwoosh.InApp;
using Pushwoosh.Notification;

namespace Pushwoosh.Droid
{
    public class PushManager : Pushwoosh.PushManager
    {
        internal static event EventHandler InitFinished;
        public static new PushManager Instance => global::Pushwoosh.PushManager.Instance as PushManager;

        public static PushwooshError ErrorFromJavaException(Java.Lang.Exception error)
        {
            return error != null ? new PushwooshError { NativeError = error, Description = error.Message } : null;
        }

        public static PushwooshError ErrorFromJavaException(Java.Lang.Object error)
        {
            return ErrorFromJavaException(error.JavaCast<Java.Lang.Exception>());
        }

        public static PushNotification NotificationFromNative(Notification.PushMessage message) 
        {
            return new PushNotification()
            {
                Payload = message.ToJson().ToString(),
                Title = message.Header,
                Message = message.Message,
                CustomData = message.CustomData
            };
        }

        internal class Callback : Java.Lang.Object, ICallback
        {
            public Action<Result> ResultCallback { get; set; }

            public void Process(Result result) => ResultCallback(result);
        }

        PushNotificationsManager nativeManager;
        public PushManager()
        {
            nativeManager = PushNotificationsManager.Instance;
            InAppManager = new InAppManager(PushwooshInApp.Instance);
        }

        public bool MultiNotificationsMode
        {
            set 
            {
                PushwooshNotificationSettings.SetMultiNotificationMode(value);
            }
        }

        bool showPushNotificationsAlert = true;
        public override bool ShowPushNotificationAlerts 
        {
            get
            {
                return showPushNotificationsAlert;
            }
            set
            {
                showPushNotificationsAlert = value;
            }
        }

        public override string AppCode => nativeManager.AppId;

        public override string HardwareId => nativeManager.Hwid;

        public override string PushToken => nativeManager.PushToken;

        public override Task<RequestResult<TagsBundle>> LoadTagsAsync()
        {
            var source = new TaskCompletionSource<RequestResult<TagsBundle>>();
            nativeManager.GetTags(new Callback()
            {
                ResultCallback = (Result obj) =>
                {
                    var bundle = obj.Data as Tags.TagsBundle;
                    var tags = new TagsBundle();
                    if (bundle != null)
                    {
                        var jsonObject = bundle.ToJson();
                        var keysIterator = jsonObject.Keys();

                        while (keysIterator.HasNext)
                        {
                            var key = (keysIterator.Next() as Java.Lang.String).ToString();
                            var val = jsonObject.Get(key);
                            if (val is Java.Lang.String)
                                tags.PutString(key, val.ToString());
                            else if (val is Java.Lang.Integer)
                                tags.PutInteger(key, (val as Java.Lang.Integer).IntValue());
                            else if (val is Org.Json.JSONArray)
                            {
                                var jsonArray = val as Org.Json.JSONArray;
                                string[] list = new string[jsonArray.Length()];
                                for (int i = 0; i < list.Length; ++i)
                                {
                                    list[i] = jsonArray.Get(i).ToString();
                                }
                                tags.PutList(key, list);
                            }
                        }
                    }
                    if (obj.Exception != null)
                    {
                        source.SetResult(new RequestResult<TagsBundle>() { Error = ErrorFromJavaException(obj.Exception) });
                    }
                    else
                    {
                        source.SetResult(new RequestResult<TagsBundle>() { Result = tags });
                    }
                }
            });
            return source.Task;
        }

        public override void Register()
        {
            nativeManager.RegisterForPushNotifications();
        }

        public override void SendPurchaseData(string identifier, decimal price, string currency)
        {
            nativeManager.SendInappPurchase(identifier, new Java.Math.BigDecimal(price.ToString(CultureInfo.InvariantCulture)), currency);
        }

        public override Task<RequestResult> SendTagsAsync(TagsBundle tags)
        {
            if (tags == null)
                return null;
            var source = new TaskCompletionSource<RequestResult>();
            var dict = tags.ToDictionary;
            Tags.TagsBundle.Builder tagsBundleBuilder = new Tags.TagsBundle.Builder();
            foreach (var kv in dict)
            {
                if (kv.Value is Nullable<int>)
                    tagsBundleBuilder.PutInt(kv.Key, ((int?)kv.Value).Value);
                else if (kv.Value is string)
                    tagsBundleBuilder.PutString(kv.Key, (string)kv.Value);
                else if (kv.Value is TagsBundle.IncrementalInteger)
                    tagsBundleBuilder.IncrementInt(kv.Key, ((TagsBundle.IncrementalInteger)kv.Value).Value);
                else if (kv.Value is IList<string>)
                    tagsBundleBuilder.PutList(kv.Key, (IList<string>)kv.Value);
            }
            nativeManager.SendTags(tagsBundleBuilder.Build(), new Callback() { ResultCallback = (Result obj) => {
                source.SetResult(new RequestResult { Error = ErrorFromJavaException(obj.Exception) });
                } } );
            return source.Task;
        }

        public override void Unregister()
        {
            nativeManager.UnregisterForPushNotifications();
        }

        protected override PushNotificationSettings GetNotificationSettings()
        {
            return new PushNotificationSettings() { Enabled = Notification.PushwooshNotificationSettings.AreNotificationsEnabled() };
        }

        public override int ScheduleLocalNotification(string body, int delay)
        {
            var notifiaction = new Notification.LocalNotification.Builder().SetMessage(body).SetDelay(delay).Build();
            return PushNotificationsManager.Instance.ScheduleLocalNotification(notifiaction).RequestId;
        }

        public override void CancelLocalNotification(int notificationId)
        {
            Notification.LocalNotificationReceiver.CancelNotification(notificationId);
        }

        public override void CancelAllLocalNotifications()
        {
            Notification.LocalNotificationReceiver.CancelAll();
        }

        public static void Init()
        {
            global::Pushwoosh.PushManager.Instance = new PushManager();
            if (InitFinished != null)
                InitFinished(null, null);
        }

        public override void StartLocationTracking()
        {
            Location.PushwooshLocation.StartLocationTracking();
        }

        public override void StopLocationTracking()
        {
            Location.PushwooshLocation.StopLocationTracking();
        }
    }
}
