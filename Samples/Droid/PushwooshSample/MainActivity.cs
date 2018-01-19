using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;
using Android.Util;
using Org.Json;
using Pushwoosh;
using Pushwoosh.InApp;
using Pushwoosh.Badge;
using Pushwoosh.Tags;
using System.Collections.Generic;

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

			// Reset application icon badge number
            PushwooshBadge.BadgeNumber = 0;

            PushwooshInApp.Instance.UserId = "%userId%";

            PushwooshInApp.Instance.PostEvent("applicationOpened", new TagsBundle.Builder().PutString("attribute", "value").Build());

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.main);

			mGeneralStatus = FindViewById<TextView>(Resource.Id.general_status);
			mTagsStatus = FindViewById<TextView>(Resource.Id.status);

		}

		public void doOnRegistered(String registrationId)
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
	}
}
