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
using Com.Pushwoosh.Inapp;
using System.Collections.Generic;

namespace PushwooshSample
{
	class LocalMessageBroadcastReceiver : BasePushMessageReceiver
	{
		public MainActivity activity {get; set;}

		protected override void OnMessageReceive (Intent intent)
		{
			activity.doOnMessageReceive (intent.GetStringExtra (BasePushMessageReceiver.JsonDataKey));
		}
	}

	class LocalRegisterBroadcastReceiver : BaseRegistrationReceiver
	{
		public MainActivity activity {get; set;}

		protected override void OnRegisterActionReceive (Context p0, Intent intent)
		{
			activity.checkMessage (intent);
		}
	}

	[Activity (Label = "PushwooshSample", MainLauncher = true)]
	[IntentFilter (new string[]{"com.pushwoosh.test.xamarin.app.MESSAGE"}, Categories = new string[]{"android.intent.category.DEFAULT"})]
	public class MainActivity : FragmentActivity
	{
		TextView mTagsStatus;
		TextView mGeneralStatus;

		LocalMessageBroadcastReceiver mMessageReceiver;
		LocalRegisterBroadcastReceiver mRegisterReceiver;

		bool mBroadcastPush = true;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			mMessageReceiver = new LocalMessageBroadcastReceiver ();
			mMessageReceiver.activity = this;

			mRegisterReceiver = new LocalRegisterBroadcastReceiver ();
			mRegisterReceiver.activity = this;

			registerReceivers ();

			PushManager manager = PushManager.GetInstance (this);
			manager.OnStartup (this);

			// Register for push!
			manager.RegisterForPushNotifications();

			// Reset application icon badge number
			manager.BadgeNumber = 0;

			manager.SetUserId(this, "%userId%");

			IDictionary<string, Java.Lang.Object> attributes = new Dictionary<string, Java.Lang.Object>();
			attributes.Add("attribute", new Java.Lang.String("value"));
			InAppFacade.PostEvent(this, "applicationOpened", attributes);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.main);

			mGeneralStatus = FindViewById<TextView>(Resource.Id.general_status);
			mTagsStatus = FindViewById<TextView>(Resource.Id.status);

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
				else if (intent.HasExtra(PushManager.RegisterEvent))
				{
					doOnRegistered(intent.Extras.GetString(PushManager.RegisterEvent));
				}
				else if (intent.HasExtra(PushManager.UnregisterEvent))
				{
					doOnUnregisteredError(intent.Extras.GetString(PushManager.UnregisterEvent));
				}
				else if (intent.HasExtra(PushManager.RegisterErrorEvent))
				{
					doOnRegisteredError(intent.Extras.GetString(PushManager.RegisterErrorEvent));
				}
				else if (intent.HasExtra(PushManager.UnregisterErrorEvent))
				{
					doOnUnregistered(intent.Extras.GetString(PushManager.UnregisterErrorEvent));
				}

				resetIntentValues();
			}
		}

		public void doOnRegistered(String registrationId)
		{
			string reg = GetString (Resource.String.registered);
			reg = reg.Replace ("%s", registrationId);
			mGeneralStatus.Text = reg;

			//			mGeneralStatus.SetText(GetString(Resource.String.registered, registrationId));
		}

		public void doOnRegisteredError(String errorId)
		{
			mGeneralStatus.SetText(Resource.String.registered_error);
			//			mGeneralStatus.SetText(GetString(Resource.String.registered_error, errorId));
		}

		public void doOnUnregistered(String registrationId)
		{
			mGeneralStatus.SetText(Resource.String.unregistered);
			//			mGeneralStatus.SetText(GetString(Resource.String.unregistered, registrationId));
		}

		public void doOnUnregisteredError(String errorId)
		{
			mGeneralStatus.SetText(Resource.String.unregistered_error);
			//			mGeneralStatus.SetText(GetString(Resource.String.unregistered_error, errorId));
		}

		public void doOnMessageReceive(String message)
		{
			string me = GetString (Resource.String.on_message);
			me = me.Replace("%s", message);
			mGeneralStatus.Text = me;

			//			mGeneralStatus.SetText(GetString(Resource.String.on_message, message));

			// Parse custom JSON data string.
			// You can set background color with custom JSON data in the following format: { "r" : "10", "g" : "200", "b" : "100" }
			// Or open specific screen of the app with custom page ID (set ID in the { "id" : "2" } format)

			try
			{

				JSONObject messageJson = new JSONObject(message);
				JSONObject customJson = new JSONObject(messageJson.GetString("u"));

				if (customJson.Has("r") && customJson.Has("g") && customJson.Has("b"))
				{
					int r = customJson.GetInt("r");
					int g = customJson.GetInt("g");
					int b = customJson.GetInt("b");
					Window.DecorView.FindViewById<View>(Android.Resource.Id.Content).SetBackgroundColor(Android.Graphics.Color.Rgb(r, g, b));
				}

				if (customJson.Has("id"))
				{
					Intent intent = new Intent(this, typeof(SecondActivity));
					intent.PutExtra(PushManager.PushReceiveEvent, messageJson.ToString());
					StartActivity(intent);
				}
			}
			catch (JSONException e) {
				e.PrintStackTrace ();
			}
		}

		/**
	 * Will check main Activity intent and if it contains any PushWoosh data,
	 * will clear it
	 */
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

			if (mBroadcastPush)
			{
				RegisterReceiver(mMessageReceiver, intentFilter);
			}

			RegisterReceiver(mRegisterReceiver, new IntentFilter(PackageName + "." + PushManager.RegisterBroadCastAction));
		}

		public void unregisterReceivers()
		{
			UnregisterReceiver(mMessageReceiver);
			UnregisterReceiver(mRegisterReceiver);
		}

	}
}
