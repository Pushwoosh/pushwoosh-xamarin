using System;
using System.Drawing;
using ObjCRuntime;
using Foundation;
using UIKit;
using CoreLocation;

namespace Pushwoosh
{
	public delegate void LocationHandler (CLLocation location);
	public delegate void PushwooshGetTagsHandler (NSDictionary tags);
	public delegate void PushwooshErrorHandler (NSError error);

	[Model, BaseType (typeof (NSObject))]
	public partial interface HtmlWebViewControllerDelegate {

		[Export ("htmlWebViewControllerDidClose:")]
		void HtmlWebViewControllerDidClose (PWHtmlWebViewController viewController);
	}

	[BaseType (typeof (UIViewController))]
	public partial interface PWHtmlWebViewController  {

		[Export ("initWithURLString:")]
		IntPtr Constructor (NSString url);

		[Export ("delegate", ArgumentSemantic.Assign)]
		HtmlWebViewControllerDelegate Delegate { get; set; }

		[Export ("webview", ArgumentSemantic.Retain)]
		UIWebView Webview { get; set; }

		[Export ("activityIndicator", ArgumentSemantic.Retain)]
		UIActivityIndicatorView ActivityIndicator { get; set; }

		[Export ("supportedOrientations")]
		PWSupportedOrientations SupportedOrientations { get; set; }
	}
	
	[Model]
	public partial interface PushNotificationDelegate {

		[Export ("onDidRegisterForRemoteNotificationsWithDeviceToken:")]
		void  DidRegisterForRemoteNotificationsWithDeviceToken (NSString token);

		[Export ("onDidFailToRegisterForRemoteNotificationsWithError:")]
		void  DidFailToRegisterForRemoteNotificationsWithError (NSError error);

		[Export ("onPushReceived:withNotification:onStart:")]
		void PushReceivedWithNotificationOnStart (PushNotificationManager pushManager, NSDictionary pushNotification, bool onStart);

		[Export ("onPushAccepted:withNotification:")]
		void PushAcceptedWithNotification (PushNotificationManager pushManager, NSDictionary pushNotification);

		[Export ("onPushAccepted:withNotification:onStart:")]
		void PushAcceptedWithNotificationOnStart (PushNotificationManager pushManager, NSDictionary pushNotification, bool onStart);

		[Export ("onTagsReceived:")]
		void  TagsReceived (NSDictionary tags);

		[Export ("onTagsFailedToReceive:")]
		void  TagsFailedToReceive (NSError error);
	}


	[BaseType (typeof (NSObject))]
	public partial interface PWTags {

		[Static, Export ("incrementalTagWithInteger:")]
		NSDictionary IncrementalTagWithInteger (nint delta);
	}

	[BaseType (typeof (NSObject))]
	public partial interface PushNotificationManager {

		[Export ("appCode", ArgumentSemantic.Copy)]
		NSString AppCode { get; set; }

		[Export ("appName", ArgumentSemantic.Copy)]
		NSString AppName { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)]
		NSObject Delegate { get; set; }

		[Export ("richPushWindow", ArgumentSemantic.Retain)]
		UIWindow RichPushWindow { get; set; }

		[Export ("pushNotifications", ArgumentSemantic.Retain)]
		NSDictionary PushNotifications { get; set; }

		[Export ("supportedOrientations")]
		PWSupportedOrientations SupportedOrientations { get; set; }

		[Export ("showPushnotificationAlert")]
		bool ShowPushnotificationAlert { get; set; }

		[Static, Export ("initializeWithAppCode:appName:")]
		void InitializeWithAppCodeAndName (NSString appCode, NSString appName);

		[Static, Export ("pushManager")]
		PushNotificationManager PushManager { get; }

		[Static, Export ("getAPSProductionStatus")]
		bool GetAPSProductionStatus { get; }

		[Export ("initWithApplicationCode:appName:")]
		IntPtr Constructor (NSString appCode, NSString appName);

		[Export ("initWithApplicationCode:navController:appName:")]
		IntPtr Constructor (NSString appCode, UIViewController navController, NSString appName);

		[Export ("showWebView")]
		void ShowWebView ();

		[Export ("registerForPushNotifications")]
		void RegisterForPushNotifications ();

		[Export ("unregisterForPushNotifications")]
		void UnregisterForPushNotifications ();

		[Export ("startLocationTracking")]
		void StartLocationTracking ();

		[Export ("stopLocationTracking")]
		void StopLocationTracking ();

		[Export ("tags")]
		NSDictionary Tags { set; }

		[Export ("loadTags")]
		void LoadTags ();

		[Export ("loadTags:error:")]
		void LoadTags (PushwooshGetTagsHandler successHandler, PushwooshErrorHandler errorHandler);

		[Export ("sendAppOpen")]
		void SendAppOpen ();

		[Export ("sendBadges:")]
		void SendBadges (nint badge);

		[Export ("sendLocation:")]
		void SendLocation (CLLocation location);

		[Export ("recordGoal:")]
		void RecordGoal (NSString goal);

		[Export ("recordGoal:withCount:")]
		void RecordGoal (NSString goal, NSNumber count);

		[Export ("getPushToken")]
		string GetPushToken { get; }

		[Export ("getHWID")]
		string GetHWID { get; }

		[Export ("handlePushRegistration:")]
		void HandlePushRegistration (NSData devToken);

		[Export ("handlePushRegistrationString:")]
		void HandlePushRegistrationString (NSString deviceID);

		[Export ("handlePushRegistrationFailure:")]
		void HandlePushRegistrationFailure (NSError error);

		[Export ("handlePushReceived:")]
		bool HandlePushReceived (NSDictionary userInfo);

		[Export ("getApnPayload:")]
		NSDictionary GetApnPayload (NSDictionary pushNotification);

		[Export ("getCustomPushData:")]
		string GetCustomPushData (NSDictionary pushNotification);

		[Static, Export ("clearNotificationCenter")]
		void ClearNotificationCenter ();

		[Export("setUserId:")]
		void SetUserId(NSString userId);

		[Export("postEvent:withAttributes:")]
		void PostEvent(NSString eventId, NSDictionary attributes);
	}
}

