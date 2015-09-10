
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;
using Pushwoosh;

namespace PushwooshSample
{
	[BroadcastReceiver(Enabled=true, Permission=".permission.C2D_MESSAGE")]
	[IntentFilter(new[] { "com.pushwoosh.test.xamarin.app.action.SILENT_PUSH_RECEIVE" })]
	public class SilentPushReceiver : BroadcastReceiver
	{
		public override void OnReceive (Context context, Intent intent)
		{
			Log.Debug ("PushwooshSample", intent.GetStringExtra(BasePushMessageReceiver.JsonDataKey));
		}
	}
}

