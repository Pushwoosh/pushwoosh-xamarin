using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;
using Pushwoosh;
using Pushwoosh.InApp;
using Pushwoosh.Badge;
using Pushwoosh.Tags;
using Pushwoosh.Location;
using Pushwoosh.Inbox.UI.Activity;
using Pushwoosh.Inbox.UI;
using Android.Graphics;
using Pushwoosh.Inbox;
using Java.Util;
using Java.Text;
using Java.Lang;
using String = Java.Lang.String;
using Object = Java.Lang.Object;

namespace PushwooshSample
{
	[Activity (Label = "PushwooshSample", MainLauncher = true)]
	public class MainActivity : FragmentActivity
	{
		TextView mTagsStatus;
		TextView mGeneralStatus;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Register for push!
            PushNotificationsManager.Instance.RegisterForPushNotifications(new CallbackWrapper() { Callback = (result) => {
                    if (result.IsSuccess) {
                        doOnRegistered((string)result.Data);
                    } else {
                        doOnRegisteredError(result.Exception.ToString());
                    }
                } });

            //PushNotificationsManager.Instance.Language = "fr";

            // Reset application icon badge number
            PushwooshBadge.BadgeNumber = 0;

            PushwooshInApp.Instance.UserId = "%userId%";

            PushwooshInApp.Instance.PostEvent("applicationOpened", new TagsBundle.Builder().PutString("attribute", "value").Build());

            // Start tracking geozones
            PushwooshLocation.StartLocationTracking();

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.main);

			mGeneralStatus = FindViewById<TextView>(Resource.Id.general_status);
			mTagsStatus = FindViewById<TextView>(Resource.Id.status);

            PushNotificationsManager.Instance.SendTags(new TagsBundle.Builder().PutString("key", "value").Build(), null);

            FindViewById<Button>(Resource.Id.inbox).Click += delegate
            {
                // Set inbox style and open inbox activity
                SetInboxStyle();
                OpenInboxActivity();
            };
		}

		public void doOnRegistered(string registrationId)
		{
			string reg = GetString (Resource.String.registered);
			reg = reg.Replace ("%s", registrationId);
			mGeneralStatus.Text = reg;
        }

		public void doOnRegisteredError(string errorText)
		{
            mGeneralStatus.SetText(errorText, TextView.BufferType.Normal);
		}

        protected override void OnResume()
        {
            base.OnResume();
            NotificationServiceExtension.CurrentActivity = this;
        }

		protected override void OnPause()
        {
            base.OnPause();
            if (NotificationServiceExtension.CurrentActivity == this)
                NotificationServiceExtension.CurrentActivity = null;
        }

        private void SetInboxStyle()
        {
            PushwooshInboxStyle style = PushwooshInboxStyle.Instance;
            style.DateFormatter = new CustomDateFormatter();

            style.AccentColor = new Integer(Color.ParseColor("#ff00ff"));
            style.BackgroundColor = new Integer(Color.GhostWhite);
            style.BarAccentColor = new Integer(Color.NavajoWhite);
            style.BarBackgroundColor = new Integer(Color.MediumPurple);
            style.BarTextColor = new Integer(Color.Black);
            style.DateColor = new Integer(Color.DarkGray);
            style.DescriptionColor = new Integer(Color.Black);
            style.DividerColor = new Integer(Color.ParseColor("#ff00ff"));
            style.HighlightColor = new Integer(Color.ParseColor("#ff00ff"));
            style.ImageTypeColor = new Integer(Color.Honeydew);
            style.ReadDateColor = new Integer(Color.DarkGray);
            style.ReadDescriptionColor = new Integer(Color.ParseColor("#ff44ff"));
            style.ReadImageTypeColor = new Integer(Color.ParseColor("#ff44ff"));
            style.ReadTitleColor = new Integer(Color.ParseColor("#ff44ff"));
            style.TitleColor = new Integer(Color.ParseColor("#ff00ff"));

            style.BarTitle = "My title";
            style.ListEmptyText = "There is no messages here";
            style.ListErrorMessage = "Some error happenned";

            style.DefaultImageIcon = Resource.Drawable.inbox_message;
            style.ListEmptyImage = Resource.Drawable.inbox_empty;
            style.ListErrorImage = Resource.Drawable.inbox_error;
            style.TitleTextSize = new Float(20);
            style.DescriptionTextSize = new Float(18);
            style.DateTextSize = new Float(12);
        }

        private void OpenInboxActivity()
        {
            Intent intent = new Intent(this, typeof(InboxActivity));
            StartActivity(intent);
        }

        private class CustomDateFormatter : Object, IInboxDateFormatter
        {
            private SimpleDateFormat sdf = new SimpleDateFormat("dd.MM.yyyy");
            public String Transform(Date date)
            {
                return new String(sdf.Format(date));
            }
        }
    }
}
