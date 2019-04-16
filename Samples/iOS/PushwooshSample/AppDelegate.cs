using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using Pushwoosh;
using Pushwoosh.Geozones.iOS.Bindings;
using UserNotifications;

namespace PushwooshSample
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		public PushwooshSampleViewController viewController;
        public PushDelegate _pushDelegate;

		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			viewController = new PushwooshSampleViewController ();
			window.RootViewController = viewController;
			window.MakeKeyAndVisible ();

			PushNotificationManager pushmanager = PushNotificationManager.PushManager;
            _pushDelegate = new PushDelegate();
            pushmanager.Delegate = _pushDelegate;

            //pushmanager.Language = (NSString)"es";

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
            pushmanager.RegisterForPushNotifications();

            //Start tracking Geozones
            PWGeozonesManager.SharedManager.StartLocationTracking();

            pushmanager.SetUserId(new NSString("%userId%"));

            pushmanager.PostEvent(new NSString("applicationFinishedLaunching"), new NSDictionary("attribute", "value"));

            PWInAppManager inappManager = PWInAppManager.SharedManager;
            inappManager.AddJavascriptInterface(new JavaScriptInterface(), new NSString("jsInterface"));
            inappManager.PostEvent(new NSString("1"), new NSDictionary());

            Console.WriteLine("HWID: " + pushmanager.HWID);

            return true;
		}

		public override void RegisteredForRemoteNotifications (UIApplication application, NSData deviceToken)
		{
			PushNotificationManager.PushManager.HandlePushRegistration (deviceToken);
		}

		public override void FailedToRegisterForRemoteNotifications (UIApplication application , NSError error)
		{
			PushNotificationManager.PushManager.HandlePushRegistrationFailure (error);
		}

		public override void ReceivedRemoteNotification (UIApplication application, NSDictionary userInfo)		
		{
			PushNotificationManager.PushManager.HandlePushReceived (userInfo);
		}
	}

	public class PushDelegate : PushNotificationDelegate
	{
		public override void OnPushAccepted(PushNotificationManager pushManager, NSDictionary pushNotification)
		{
			Console.WriteLine("Push accepted: " + pushNotification);
		}

        public override void OnPushReceived(PushNotificationManager pushManager, NSDictionary pushNotification, bool onStart)
        {
            Console.WriteLine("Push received: " + pushNotification);

            var okAlertController = UIAlertController.Create("Push received", pushNotification.Description, UIAlertControllerStyle.Alert);
            okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
           ((AppDelegate)UIApplication.SharedApplication.Delegate).viewController.PresentViewController(okAlertController, true, null);
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

