﻿using System;
using Foundation;
using Pushwoosh.Inbox;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Pushwoosh;

namespace Pushwoosh.Inbox.iOS
{
    public class InboxManager : Inbox.InboxManager
    {
        public static new InboxManager Instance => Inbox.InboxManager.Instance as InboxManager;

        public static void Init()
        {
            Inbox.InboxManager.Instance = new InboxManager();
        }

        public override void PresentInboxUI(PushwooshInboxStyle style) 
        {
            PWIInboxStyle nativeStyle = GetNativeStyle(style);
            PWIInboxViewController inboxViewController = PWIInboxUI.CreateInboxControllerWithStyle(nativeStyle);
            inboxViewController.NavigationItem.LeftBarButtonItem = new UIBarButtonItem("Close", UIBarButtonItemStyle.Plain, OnCloseInboxClick);
            UIViewController rootViewController = FindRootViewController();
            UINavigationController navigationController = new UINavigationController(inboxViewController);
            rootViewController.PresentViewController(navigationController, true, null);
      
        }

        public override void UnreadMessagesCountWithCompletion(Action<int, string> completion)
        {
            PWInbox.UnreadMessagesCountWithCompletion((nint count, NSError error) => {
                completion((int)count, error != null ? error.LocalizedDescription : null);
            });
        }

        public override void AddObserverForUnreadMessagesCount(Action<int> completion)
        {
            PWInbox.AddObserverForUnreadMessagesCount((nint count) => {
                completion((int)count);
            });
        }

        private PWIInboxStyle GetNativeStyle(PushwooshInboxStyle style)
        {
            PWIInboxStyle nativeStyle = PWIInboxStyle.DefaultStyle();
            if (style.AccentColor != Color.Default)
            {
                nativeStyle.AccentColor = GetUIColor(style.AccentColor);
            }
            if (style.BackgroundColor != Color.Default)
            {
                nativeStyle.BackgroundColor = GetUIColor(style.BackgroundColor);
            }
            if (style.BarAccentColor != Color.Default)
            {
                nativeStyle.BarAccentColor = GetUIColor(style.BarAccentColor);
            }
            if (style.BarBackgroundColor != Color.Default)
            {
                nativeStyle.BarBackgroundColor = GetUIColor(style.BarBackgroundColor);
            }
            if (style.BarTextColor != Color.Default)
            {
                nativeStyle.BarTextColor = GetUIColor(style.BarTextColor);
            }
            if (!string.IsNullOrEmpty(style.BarTitle))
            {
                nativeStyle.BarTitle = style.BarTitle;
            }
            if (style.DateColor != Color.Default)
            {
                nativeStyle.DateColor = GetUIColor(style.DateColor);
            }
            if (!string.IsNullOrEmpty(style.DateFormat))
            {
                NSDateFormatter formatter = new NSDateFormatter
                {
                    DateFormat = style.DateFormat
                };
                nativeStyle.DateFormatterBlock = (NSDate date, NSObject owner) => formatter.StringFor(date);
            }
            if (!string.IsNullOrEmpty(style.DefaultImageName))
            {
                nativeStyle.DefaultImageIcon = GetUIImage(style.DefaultImageName);
            }

            if (style.DescriptionColor != Color.Default)
            {
                nativeStyle.DescriptionColor = GetUIColor(style.DescriptionColor);
            }

            if (!string.IsNullOrEmpty(style.ListEmptyImageName))
            {
                nativeStyle.ListEmptyImage = GetUIImage(style.ListEmptyImageName);
            }

            if (!string.IsNullOrEmpty(style.ListEmptyMessage))
            {
                nativeStyle.ListEmptyMessage = style.ListEmptyMessage;
            }

            if (!string.IsNullOrEmpty(style.ListErrorImageName))
            {
                nativeStyle.ListErrorImage = GetUIImage(style.ListErrorImageName);
            }

            if (!string.IsNullOrEmpty(style.ListErrorMessage))
            {
                nativeStyle.ListErrorMessage = style.ListErrorMessage;
            }

            if (style.SeparatorColor != Color.Default)
            {
                nativeStyle.SeparatorColor = GetUIColor(style.SeparatorColor);
            }

            if (style.TitleColor != Color.Default)
            {
                nativeStyle.TitleColor = GetUIColor(style.TitleColor);
            }
            if (style.TitleTextSize > 0)
            {
                nativeStyle.TitleFont = UIFont.SystemFontOfSize(style.TitleTextSize);
            }
            if (style.DateTextSize > 0)
            {
                nativeStyle.DateFont = UIFont.SystemFontOfSize(style.DateTextSize);
            }
            if (style.DescriptionTextSize > 0)
            {
                nativeStyle.DescriptionFont = UIFont.SystemFontOfSize(style.DescriptionTextSize);
            }
            return nativeStyle;
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

        private UIColor GetUIColor(object colorObj)
        {
            return ((Color)colorObj).ToUIColor();
        }

        private UIImage GetUIImage(object imageNameObj)
        {
            return UIImage.FromBundle((string)imageNameObj);
        }
    }
}
