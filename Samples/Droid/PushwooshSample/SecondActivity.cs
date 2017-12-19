using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;
using Org.Json;
using Pushwoosh;


namespace PushwooshSample
{
	[Activity]
	public class SecondActivity : FragmentActivity
	{
        public const string PUSH_MESSAGE_KEY = "PUSH_MESSAGE_KEY";
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.second);

            FindViewById<TextView> (Resource.Id.text_push).Text = Intent.GetStringExtra (PUSH_MESSAGE_KEY);
		}
	}
}

