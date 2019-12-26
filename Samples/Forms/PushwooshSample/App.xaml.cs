using Xamarin.Forms;
using Pushwoosh.Geozones;
using Pushwoosh;
using Pushwoosh.Inbox;

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
            PushwooshInboxStyle inboxStyle = new PushwooshInboxStyle
            {
                AccentColor = Color.Violet,
                BackgroundColor = Color.White,
                BarAccentColor = Color.Blue,
                BarBackgroundColor = Color.WhiteSmoke,
                BarTextColor = Color.DarkGray,
                DateColor = Color.Violet,
                DefaultTextColor = Color.DarkBlue,
                DescriptionColor = Color.DarkBlue,
                SelectionColor = Color.Crimson,
                SeparatorColor = Color.Crimson,
                TitleColor = Color.DarkKhaki,

                DefaultImageName = "inbox_message",
                ListEmptyImageName = "inbox_empty",
                ListErrorImageName = "inbox_error",
                UnreadImageName = "inbox_unread",

                BarTitle = "My custom title",
                ListEmptyMessage = "There are no inbox messages yet",
                ListErrorMessage = "Some error happened",

                DateFormat = "dd.MM.yyyy",

                TitleTextSize = 20,
                DateTextSize = 12,
                DescriptionTextSize = 18
            };

            InboxManager.Instance.PresentInboxUI(inboxStyle);
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
