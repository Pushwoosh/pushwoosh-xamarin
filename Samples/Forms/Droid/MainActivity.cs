using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

[assembly:MetaData("com.pushwoosh.appid", Value = "DC533-F5DA4")]
[assembly:MetaData("com.pushwoosh.senderid", Value = "@string/fcm_sender_id")]
namespace PushwooshSample.Droid
{
    [Activity(Label = "PushwooshSample.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Pushwoosh.Droid.PushManager.Init();
            Pushwoosh.Geozones.Droid.GeozonesManager.Init();

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());
        }
    }
}
