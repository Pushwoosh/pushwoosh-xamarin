using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Pushwoosh.iOS;

namespace PushwooshSample.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());
            PushManager.Init();

            return base.FinishedLaunching(app, options);
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            PushManager.Instance.RegisteredForRemoteNotifications(deviceToken);
        }

        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            PushManager.Instance.FailedToRegisterForRemoteNotifications(error);
        }

        public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
        {
            PushManager.Instance.ReceivedRemoteNotification(userInfo);
        }
    }
}
