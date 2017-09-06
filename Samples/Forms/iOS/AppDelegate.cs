using System;
using System.Collections.Generic;
using System.Linq;
using Pushwoosh;

using Foundation;
using UIKit;
using UserNotifications;

namespace PushwooshSample.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
        PushDelegate _pushDelegate;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

			LoadApplication (new App ());

			PushNotificationManager pushmanager = PushNotificationManager.PushManager;
            _pushDelegate = new PushDelegate();
            pushmanager.Delegate = _pushDelegate;

			if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
			{
                UNUserNotificationCenter.Current.Delegate = pushmanager.NotificationCenterDelegate;
			}

			if (options != null) {
				if (options.ContainsKey (UIApplication.LaunchOptionsRemoteNotificationKey)) { 
					pushmanager.HandlePushReceived (options);
				}
			}

			pushmanager.SendAppOpen ();
			pushmanager.RegisterForPushNotifications ();

			pushmanager.StartLocationTracking ();

			pushmanager.SetUserId(new NSString("%userId%"));

			pushmanager.PostEvent(new NSString("applicationFinishedLaunching"), new NSDictionary("attribute", "value"));

			PWInAppManager inappManager = PWInAppManager.SharedManager;
			inappManager.AddJavascriptInterface(new JavaScriptInterface(), new NSString("jsInterface"));
			inappManager.PostEvent(new NSString("1"), new NSDictionary());

            Console.WriteLine("HWID: " + pushmanager.HWID);

			return base.FinishedLaunching (app, options);
		}

		public override void RegisteredForRemoteNotifications (UIApplication application, NSData deviceToken)
		{
			PushNotificationManager.PushManager.HandlePushRegistration (deviceToken);
		}

		public override void FailedToRegisterForRemoteNotifications (UIApplication application , NSError error)
		{
			Console.WriteLine ("Error: " + error);
			PushNotificationManager.PushManager.HandlePushRegistrationFailure (error);
		}

        public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
        {
            PushNotificationManager.PushManager.HandlePushReceived(userInfo);
        }

		public class PushDelegate : PushNotificationDelegate
		{
			public override void OnPushAccepted(PushNotificationManager pushManager, NSDictionary pushNotification)
			{
				Console.WriteLine("Push accepted: " + pushNotification);
			}

			public override void OnDidRegisterForRemoteNotificationsWithDeviceToken(NSString token)
			{
				Console.WriteLine("Registered for push notifications: " + token);
			}

			public override void OnDidFailToRegisterForRemoteNotificationsWithError(NSError error)
			{
				Console.WriteLine("Error: " + error);
			}
		}

		public class JavaScriptInterface : PWJavaScriptInterface
		{
			public override void OnWebViewStartLoad(UIWebView webView)
			{
				Console.WriteLine("onWebViewStartLoad");
			}

			public override void OnWebViewFinishLoad(UIWebView webView)
			{
				Console.WriteLine("onWebViewFinishLoad");
			}

			public override void OnWebViewStartClose(UIWebView webView)
			{
				Console.WriteLine("onWebViewStartClose");
			}
		}
	}
}

