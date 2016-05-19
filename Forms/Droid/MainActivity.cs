using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;
using Pushwoosh;

namespace PushwooshSample.Droid
{
	class LocalMessageBroadcastReceiver : BasePushMessageReceiver
	{
		public MainActivity activity {get; set;}

		protected override void OnMessageReceive (Intent intent)
		{
			activity.doOnMessageReceive (intent.GetStringExtra (BasePushMessageReceiver.JsonDataKey));
		}
	}

	[Activity (Label = "PushwooshSample.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		LocalMessageBroadcastReceiver mMessageReceiver;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

			LoadApplication (new App ());

			mMessageReceiver = new LocalMessageBroadcastReceiver ();
			mMessageReceiver.activity = this;

			registerReceivers ();

			PushManager manager = PushManager.GetInstance (this);
			manager.OnStartup (this);

			// Register for push!
			manager.RegisterForPushNotifications();

			// Reset application icon badge number
			manager.BadgeNumber = 0;

			checkMessage (Intent);
		}

		protected override void OnNewIntent(Intent intent)
		{
			checkMessage (intent);
		}

		public void checkMessage(Intent intent)
		{
			if (null != intent)
			{
				if (intent.HasExtra(PushManager.PushReceiveEvent))
				{
					doOnMessageReceive(intent.Extras.GetString(PushManager.PushReceiveEvent));
				}

				resetIntentValues();
			}
		}
			
		public void doOnMessageReceive(String message)
		{
			// hadle push open here
			Log.Debug("PushwooshSample", "Push opened: " + message);
		}

		private void resetIntentValues()
		{
			Intent mainAppIntent = Intent;

			if (mainAppIntent.HasExtra(PushManager.PushReceiveEvent))
			{
				mainAppIntent.RemoveExtra(PushManager.PushReceiveEvent);
			}
			else if (mainAppIntent.HasExtra(PushManager.RegisterEvent))
			{
				mainAppIntent.RemoveExtra(PushManager.RegisterEvent);
			}
			else if (mainAppIntent.HasExtra(PushManager.UnregisterEvent))
			{
				mainAppIntent.RemoveExtra(PushManager.UnregisterEvent);
			}
			else if (mainAppIntent.HasExtra(PushManager.RegisterErrorEvent))
			{
				mainAppIntent.RemoveExtra(PushManager.RegisterErrorEvent);
			}
			else if (mainAppIntent.HasExtra(PushManager.UnregisterErrorEvent))
			{
				mainAppIntent.RemoveExtra(PushManager.UnregisterErrorEvent);
			}

			Intent = mainAppIntent;
		}

		protected override void OnResume ()
		{
			base.OnResume ();

			registerReceivers ();
		}

		protected override void OnPause ()
		{
			base.OnPause ();

			unregisterReceivers ();
		}

		public void registerReceivers()
		{
			IntentFilter intentFilter = new IntentFilter(PackageName + ".action.PUSH_MESSAGE_RECEIVE");
			RegisterReceiver(mMessageReceiver, intentFilter);
		}

		public void unregisterReceivers()
		{
			UnregisterReceiver(mMessageReceiver);
		}
	}
}

