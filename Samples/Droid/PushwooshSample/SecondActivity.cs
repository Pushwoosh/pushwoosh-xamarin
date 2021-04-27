using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Org.Json;
using Pushwoosh;
using AndroidX.Fragment.App;

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

