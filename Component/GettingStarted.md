The PushWoosh SDK for iOS makes it easier and faster to develop Pushwoosh integrated iOS apps.

## Sample

### AppDelegate.cs

```csharp
using Pushwoosh;

namespace Pushwoosh.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		
		public override UIWindow Window {
			get;
			set;
		}

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			PushNotificationManager pushmanager = PushNotificationManager.PushManager;
			pushmanager.Delegate = this;
			UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge;
			UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);

			if (options != null) {
				if (options.ContainsKey (UIApplication.LaunchOptionsRemoteNotificationKey)) { 
					pushmanager.HandlePushReceived (options);
				}
			}

			pushmanager.StartLocationTracking ();

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

		[Export ("application:didReceiveRemoteNotification:")]
		public void DidReceiveRemoteNotification (UIApplication application, NSDictionary userInfo)
		{
			PushNotificationManager.PushManager.HandlePushReceived (userInfo);
		}
	}
}

```

## Other Resources

* [Pushwoosh Documentation](http://www.pushwoosh.com/programming-push-notification)
* [Support Forums](https://community.pushwoosh.com)
* [Source Code Repository](https://github.com/Pushwoosh)
