using Xamarin.Forms;
using Pushwoosh.Geozones;
using Pushwoosh;
using System;

namespace PushwooshSample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new PushwooshSamplePage();
        }

        protected override void OnStart()
        {
            PushManager.Instance.Register();

            PushManager.Instance.InAppManager.PostEventAsync("test", null);

            GeozonesManager.Instance.StartLocationTracking();

            PushManager.Instance.PushAccepted += (object sender, PushNotificationEventArgs e) => {
                MainPage.DisplayAlert("Push Accepted", e.Notification.Payload, "OK");
            };
            PushManager.Instance.PushReceived += (object sender, PushNotificationEventArgs e) => {
                MainPage.DisplayAlert("Push Received", e.Notification.Payload, "OK");
            };
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
