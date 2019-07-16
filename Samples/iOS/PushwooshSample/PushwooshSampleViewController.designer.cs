// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace PushwooshSample
{
    [Register ("PushwooshSampleViewController")]
    partial class PushwooshSampleViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton InboxButton { get; set; }

        [Action ("InboxButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void InboxButton_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (InboxButton != null) {
                InboxButton.Dispose ();
                InboxButton = null;
            }
        }
    }
}