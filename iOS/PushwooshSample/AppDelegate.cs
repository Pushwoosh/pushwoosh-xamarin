using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using Pushwoosh;

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
		PushwooshSampleViewController viewController;
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
			pushmanager.Delegate = this;

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

			Console.WriteLine("HWID: " + pushmanager.GetHWID);

			return true;
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

		public override void ReceivedRemoteNotification (UIApplication application, NSDictionary userInfo)		
		{
			PushNotificationManager.PushManager.HandlePushReceived (userInfo);
		}
	}

}

