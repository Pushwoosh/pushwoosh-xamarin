using System;
using CoreGraphics;
using Foundation;
using Pushwoosh;
using Pushwoosh.Inbox;
using UIKit;

namespace PushwooshSample
{
	public partial class PushwooshSampleViewController : UIViewController
	{

		public PushwooshSampleViewController () : base ("PushwooshSampleViewController", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
        }

        partial void InboxButton_TouchUpInside(UIButton sender)
        {
            PWIInboxStyle style = GetInboxStyle();
            PresentInboxViewController(style);
        }

        private PWIInboxStyle GetInboxStyle()
        {
            PWIInboxStyle style = PWIInboxStyle.DefaultStyle();
            NSDateFormatter formatter = new NSDateFormatter
            {
                DateFormat = "dd.MM.yyyy"
            };
            style.DateFormatterBlock = (NSDate date, NSObject owner) => formatter.StringFor(date);
            style.AccentColor = UIColor.Blue;
            style.BackgroundColor = UIColor.White;
            style.BarAccentColor = UIColor.Blue;
            style.BarBackgroundColor = UIColor.White;
            style.BarTextColor = UIColor.Black;
            style.BarTitle = "My title";
            style.DateColor = UIColor.Black;
            style.TitleColor = UIColor.Black;
            style.SeparatorColor = UIColor.Gray;
            style.SelectionColor = UIColor.Orange;
            style.DescriptionColor = UIColor.Black;
            style.ListEmptyMessage = "There is no inbox messages";
            style.ListErrorMessage = "Some error happenned";
            style.DefaultImageIcon = UIImage.FromBundle("Default");
            style.ListEmptyImage = UIImage.FromBundle("Empty");
            style.ListErrorImage = UIImage.FromBundle("Error");
            style.UnreadImage = UIImage.FromBundle("Unread");
            return style;
        }

        private void PresentInboxViewController(PWIInboxStyle style)
        {
            PWIInboxViewController inboxViewController = PWIInboxUI.CreateInboxControllerWithStyle(style);
            inboxViewController.NavigationItem.LeftBarButtonItem = new UIBarButtonItem("Close", UIBarButtonItemStyle.Plain, OnCloseInboxClick);
            inboxViewController.OnMessageClickBlock = delegate { Console.WriteLine("Message clicked"); };
            UIViewController rootViewController = FindRootViewController();
            UINavigationController navigationController = new UINavigationController(inboxViewController);
            rootViewController.PresentViewController(navigationController, true, null);
        }

        private void OnCloseInboxClick(object sender, EventArgs args)
        {
            UIViewController topViewController = FindRootViewController();
            if (topViewController is UINavigationController && (((UINavigationController)topViewController).ViewControllers[0] is PWIInboxViewController))
            {
                topViewController.DismissViewController(true, null);
            }
        }

        private UIViewController FindRootViewController()
        {
            UIViewController controller = UIApplication.SharedApplication.KeyWindow.RootViewController;
            while (controller.PresentedViewController != null)
            {
                controller = controller.PresentedViewController;
            }
            return controller;
        }
    }
}

