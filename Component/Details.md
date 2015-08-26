Pushwoosh is a free multi-platform push notification service, which enables developers, marketing reps and product owners to keep in touch with their app users, drive engagement, promote products, push up sales, and track the progress of the campaign with notifications. Providing almost instant access to the service, we take the load off developers who can focus on creating beautiful products, deploy them on multiple platforms and support push notifications to all these platforms at the same time.

The PushWoosh SDK for iOS makes it easier and faster to develop Pushwoosh integrated iOS apps.

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

* [Pushwoosh SDK Documentation](https://rawgit.com/Pushwoosh/pushwoosh-ios-sdk/master/Documentation/index.html)

