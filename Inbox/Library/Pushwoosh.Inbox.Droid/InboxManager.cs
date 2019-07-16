using Android.Content;
using Java.Lang;
using Java.Text;
using Java.Util;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Application = Android.App.Application;
using Pushwoosh.Inbox.UI.Formatter;
using Pushwoosh.Inbox.UI.Activity;

namespace Pushwoosh.Inbox.Droid
{
    public class InboxManager : Inbox.InboxManager
    {
        public static new InboxManager Instance => Inbox.InboxManager.Instance as InboxManager;

        public static void Init()
        {
            Inbox.InboxManager.Instance = new InboxManager();
        }

        public override void PresentInboxUI(Inbox.PushwooshInboxStyle style)
        {
            SetStyle(style);
            Intent intent = new Intent(Application.Context, typeof(InboxActivity));
            Application.Context.StartActivity(intent);
        }

        private void SetStyle(Inbox.PushwooshInboxStyle style)
        {
            Pushwoosh.Inbox.UI.PushwooshInboxStyle nativeStyle = Pushwoosh.Inbox.UI.PushwooshInboxStyle.Instance;

            if (style.AccentColor != Color.Default) {
                nativeStyle.AccentColor = GetColor(style.AccentColor);
            }
            if (style.BackgroundColor != Color.Default) {
                nativeStyle.BackgroundColor = GetColor(style.BackgroundColor);
            }
            if (style.BarAccentColor != Color.Default)
            {
                nativeStyle.BarAccentColor = GetColor(style.BarAccentColor);
            }
            if (style.BarBackgroundColor != Color.Default)
            {
                nativeStyle.BarBackgroundColor = GetColor(style.BarBackgroundColor);
            }
            if (style.BarTextColor != Color.Default)
            {
                nativeStyle.BarTextColor = GetColor(style.BarTextColor);
            }
            if (!string.IsNullOrEmpty(style.BarTitle))
            {
                nativeStyle.BarTitle = style.BarTitle;
            }
            if (style.DateColor != Color.Default)
            {
                nativeStyle.DateColor = GetColor(style.DateColor);
            }
            if (style.SelectionColor != Color.Default)
            {
                nativeStyle.HighlightColor = GetColor(style.SelectionColor);
            }
            nativeStyle.DateFormatter = GetDateFormatter(style.DateFormat);
            if (!string.IsNullOrEmpty(style.DefaultImageName))
            {
                nativeStyle.DefaultImageIcon = GetResId(style.DefaultImageName, Application.Context);
            }
            if (style.DescriptionColor != Color.Default)
            {
                nativeStyle.DescriptionColor = GetColor(style.DescriptionColor);
            }
            if (!string.IsNullOrEmpty(style.ListEmptyImageName)) 
            {
                nativeStyle.ListEmptyImage = GetResId(style.ListEmptyImageName, Application.Context);
            }
            if (!string.IsNullOrEmpty(style.ListEmptyMessage))
            {
                nativeStyle.ListEmptyText = style.ListEmptyMessage;
            }
            if (!string.IsNullOrEmpty(style.ListErrorImageName))
            {
                nativeStyle.ListErrorImage = GetResId(style.ListErrorImageName, Application.Context);
            }
            if (!string.IsNullOrEmpty(style.ListErrorMessage))
            {
                nativeStyle.ListErrorMessage = style.ListErrorMessage;
            }
            if (style.SeparatorColor != Color.Default)
            {
                nativeStyle.DividerColor = GetColor(style.SeparatorColor);
            }
            if (style.TitleColor != Color.Default)
            {
                nativeStyle.TitleColor = GetColor(style.TitleColor);
            }
        }

        private Integer GetColor(object colorObj)
        {
            return Integer.ValueOf(((Color)colorObj).ToAndroid());
        }
        
        private int GetResId(string imageName, Context context)
        {
            return context.Resources.GetIdentifier(imageName, "drawable", context.PackageName);
        }

        
        private IInboxDateFormatter GetDateFormatter(string dateFormat)
        {
            SimpleDateFormat sdf = new SimpleDateFormat(GetDateFormatPattern(dateFormat), Locale.Default);
            InboxDateFormatter inboxDateFormatter = new InboxDateFormatter(sdf);
            return inboxDateFormatter;
        }

        private string GetDateFormatPattern(string dateFormat)
        {
            if (!string.IsNullOrEmpty(dateFormat))
            {
                return dateFormat;
            }
            return InboxDateFormatterKt.DefaultDateFormat;
        }

        private class InboxDateFormatter : Java.Lang.Object, IInboxDateFormatter
        {
            private SimpleDateFormat SimpleDateFormat;
            public InboxDateFormatter(SimpleDateFormat simpleDateFormat)
            {
                SimpleDateFormat = simpleDateFormat;
            }

            public Java.Lang.String Transform(Date dateObj)
            {
                return new Java.Lang.String(SimpleDateFormat.Format(dateObj));
            }
        }
    }
}
