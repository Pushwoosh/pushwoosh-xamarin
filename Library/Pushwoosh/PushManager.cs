using System;
using System.ComponentModel;
using System.Reflection;
using System.Threading.Tasks;

namespace Pushwoosh
{
    public abstract class PushManager
    {
		/// <summary>
		/// Occurs when push actually was receved by application
		/// </summary>
        public event EventHandler<PushNotificationEventArgs> PushReceived;
		/// <summary>
		/// Occurs when application received user tap on notification
		/// </summary>
        public event EventHandler<PushNotificationEventArgs> PushAccepted;
		/// <summary>
		/// Occurs when application successfuly registered for push notifications
		/// </summary>
		public event EventHandler Registered;
		/// <summary>
		/// Occurs when application failed to register for push notifications
		/// </summary>
        public event EventHandler<PushwooshError> RegisterError;

        /// <summary>
        /// Show push notifications alert when push notification is received while the app is running, default is `YES`
        /// </summary>
        public abstract bool ShowPushNotificationAlerts { get; set; }

		/// <summary>
		/// Pushwoosh application code
		/// </summary>
		public abstract string AppCode { get; }
		/// <summary>
		/// Unique device identifier that used in all API calls with Pushwoosh.
		/// </summary>
		public abstract string HardwareId { get; }
		/// <summary>
		/// Current push token. May be null if no push token is available yet
		/// </summary>
		public abstract string PushToken { get; }

		/// <summary>
		/// Send tags to server. Tag names have to be created in the Pushwoosh Control Panel. Possible tag types: Integer, String, Incremental (integer only), List tags (array of values).
		/// </summary>
		public abstract Task<RequestResult> SendTagsAsync(TagsBundle tags);
		/// <summary>
		/// Get tags from server
		/// </summary>
		public abstract Task<RequestResult<TagsBundle>> LoadTagsAsync();
		/// <summary>
		/// Sends In-App purchase statistics.Purchase information is stored in "In-app Product", "In-app Purchase" and "Last In-app Purchase date" default tags.
		/// </summary>
		/// <param name="identifier">purchased product ID</param>
		/// <param name="price">price for the product</param>
		/// <param name="currency">currency of the price (ex: "USD")</param>
        public abstract void SendPurchaseData(string identifier, decimal price, string currency);

        public abstract void StartLocationTracking();
        public abstract void StopLocationTracking();

		/// <summary>
		/// Register for push notifications
		/// </summary>
		public abstract void Register();
		/// <summary>
		/// Unregisters device from push notifications
		/// </summary>
		public abstract void Unregister();

        //needed for NotificationSettigns covariance
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected abstract PushNotificationSettings GetNotificationSettings();

        /// <summary>
        /// Enabled remote notificaton types
        /// </summary>
        public PushNotificationSettings NotificationSettings => GetNotificationSettings();
        /// <summary>
        /// Schedules local notification
        /// </summary>
        public abstract int ScheduleLocalNotification(string body, int delay);
        /// <summary>
        /// Cancel local notification by id
        /// </summary>
        public abstract void CancelLocalNotification(int notificationId);
        /// <summary>
        /// Cancel all local notifications
        /// </summary>
        public abstract void CancelAllLocalNotifications();

        public IInAppManager InAppManager { get; protected set; }

        static PushManager manager = null;
        public static PushManager Instance 
        {
            get 
            {
                return manager;
            }
            [EditorBrowsable(EditorBrowsableState.Never)]
            set
            {
                manager = value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void OnPushReceived(PushNotification notification, bool fromBackground)
        {
            PushReceived?.Invoke(this, new PushNotificationEventArgs() { Notification = notification, FromBackground = fromBackground });
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void OnPushAccepted(PushNotification notification, bool fromBackground)
        {
            PushAccepted?.Invoke(this, new PushNotificationEventArgs() { Notification = notification, FromBackground = fromBackground });
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void OnRegistered()
        {
            Registered?.Invoke(this, new EventArgs());
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void OnRegisterError(PushwooshError error)
        {
            RegisterError?.Invoke(this, error);
        }
    }
}
