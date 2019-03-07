using System;
using Android.Runtime;
using Android.App;
using Android.Content;
using Android.Support.V4.Content;

[assembly: Permission(Name = "${applicationId}.permission.C2D_MESSAGE", ProtectionLevel = Android.Content.PM.Protection.Signature)]

[assembly: UsesPermission("android.permission.RECEIVE_BOOT_COMPLETED")]
[assembly: MetaData("com.pushwoosh.plugin.location", Value = "com.pushwoosh.location.LocationPlugin")]

//BADGES
[assembly: MetaData("com.pushwoosh.plugin.badge", Value = "com.pushwoosh.badge.BadgePlugin")]
//samsung
[assembly: UsesPermission("com.sec.android.provider.badge.permission.READ")]
[assembly: UsesPermission("com.sec.android.provider.badge.permission.WRITE")]
//htc
[assembly: UsesPermission("com.htc.launcher.permission.READ_SETTINGS")]
[assembly: UsesPermission("com.htc.launcher.permission.UPDATE_SHORTCUT")]
//sony
[assembly: UsesPermission("com.sonyericsson.home.permission.BROADCAST_BADGE")]
[assembly: UsesPermission("com.sonymobile.home.permission.PROVIDER_INSERT_BADGE")]
//apex
[assembly: UsesPermission("com.anddoes.launcher.permission.UPDATE_COUNT")]
//solid
[assembly: UsesPermission("com.majeur.launcher.permission.UPDATE_BADGE")]
//huawei
[assembly: UsesPermission("com.huawei.android.launcher.permission.CHANGE_BADGE")]
[assembly: UsesPermission("com.huawei.android.launcher.permission.READ_SETTINGS")]
[assembly: UsesPermission("com.huawei.android.launcher.permission.WRITE_SETTINGS")]
//ZUK
[assembly: UsesPermission("android.permission.READ_APP_BADGE")]
//OPPO
[assembly: UsesPermission("com.oppo.launcher.permission.READ_SETTINGS")]
[assembly: UsesPermission("com.oppo.launcher.permission.WRITE_SETTINGS")]
//EvMe
[assembly: UsesPermission("me.everything.badger.permission.BADGE_COUNT_READ")]
[assembly: UsesPermission("me.everything.badger.permission.BADGE_COUNT_WRITE")]

namespace Pushwoosh.Location.Network
{
    [Preserve]
    [Service(Name = "com.pushwoosh.location.network.GeoLocationServiceApi16")]
    partial class GeoLocationServiceApi16 {}

    [Preserve]
    [Service(Name = "com.pushwoosh.location.network.GeoLocationServiceApi21", Exported = true, Permission = "android.permission.BIND_JOB_SERVICE")]
    partial class GeoLocationServiceApi21 { }
}

namespace Pushwoosh.Location.Internal.Utils
{
    [Preserve]
    [Activity(Name = "com.pushwoosh.location.internal.utils.ResolutionActivity", Theme = "@android:style/Theme.Translucent.NoTitleBar")]
    partial class ResolutionActivity { }
}

namespace Pushwoosh.Location.Geofencer
{
    [Preserve]
    [BroadcastReceiver(Name = "com.pushwoosh.location.geofencer.GeofenceReceiver")]
    [IntentFilter(new[] { "${applicationId}.action.GEOFENCE" } )]
    partial class GeofenceReceiver { }
}