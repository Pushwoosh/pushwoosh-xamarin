using System;
using Android.App;
using Android.Support.V4.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Org.Json;
using ArelloMobile.Push;
using ArelloMobile.Push.Utils;


namespace PushwooshSample
{
	[Activity]
	public class SecondActivity : FragmentActivity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.second);

			FindViewById<TextView> (Resource.Id.text_push).Text = Intent.GetStringExtra (PushManager.PushReceiveEvent);
		}
	}
}

