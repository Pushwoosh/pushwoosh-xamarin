using System;
using CoreLocation;
using Foundation;
using ObjCRuntime;
using UIKit;
using UserNotifications;

namespace Pushwoosh
{
    // typedef void (^PushwooshGetTagsHandler)(NSDictionary *);
    public delegate void PushwooshGetTagsHandler(NSDictionary tags);

    // typedef void (^PushwooshErrorHandler)(NSError *);
    public delegate void PushwooshErrorHandler(NSError error);

    // @protocol PushNotificationDelegate
    [Model]
    [BaseType(typeof(NSObject))]
    public interface PushNotificationDelegate
    {
        // @optional -(void)onDidRegisterForRemoteNotificationsWithDeviceToken:(NSString *)token;
        [Export("onDidRegisterForRemoteNotificationsWithDeviceToken:")]
        void OnDidRegisterForRemoteNotificationsWithDeviceToken(NSString token);

        // @optional -(void)onDidFailToRegisterForRemoteNotificationsWithError:(NSError *)error;
        [Export("onDidFailToRegisterForRemoteNotificationsWithError:")]
        void OnDidFailToRegisterForRemoteNotificationsWithError(NSError error);

        // @optional -(void)onPushReceived:(PushNotificationManager *)pushManager withNotification:(NSDictionary *)pushNotification onStart:(BOOL)onStart;
        [Export("onPushReceived:withNotification:onStart:")]
        void OnPushReceived(PushNotificationManager pushManager, NSDictionary pushNotification, bool onStart);

        // @optional -(void)onPushAccepted:(PushNotificationManager *)pushManager withNotification:(NSDictionary *)pushNotification __attribute__((deprecated("")));
        [Export("onPushAccepted:withNotification:")]
        void OnPushAccepted(PushNotificationManager pushManager, NSDictionary pushNotification);

        // @optional -(void)onPushAccepted:(PushNotificationManager *)pushManager withNotification:(NSDictionary *)pushNotification onStart:(BOOL)onStart;
        [Export("onPushAccepted:withNotification:onStart:")]
        void OnPushAccepted(PushNotificationManager pushManager, NSDictionary pushNotification, bool onStart);

        // @optional -(void)onTagsReceived:(NSDictionary *)tags;
        [Export("onTagsReceived:")]
        void OnTagsReceived(NSDictionary tags);

        // @optional -(void)onTagsFailedToReceive:(NSError *)error;
        [Export("onTagsFailedToReceive:")]
        void OnTagsFailedToReceive(NSError error);

        // @optional -(void)onInAppClosed:(NSString *)code;
        [Export("onInAppClosed:")]
        void OnInAppClosed(NSString code);

        // @optional -(void)onInAppDisplayed:(NSString *)code;
        [Export("onInAppDisplayed:")]
        void OnInAppDisplayed(NSString code);
    }

    // @interface PWTags : NSObject
    [BaseType(typeof(NSObject))]
    public interface PWTags
    {
        // +(NSDictionary *)incrementalTagWithInteger:(NSInteger)delta;
        [Static]
        [Export("incrementalTagWithInteger:")]
        NSDictionary IncrementalTagWithInteger(nint delta);
    }

    // @interface PushNotificationManager : NSObject
    [BaseType(typeof(NSObject))]
    public interface PushNotificationManager
    {
        // @property (readonly, copy, nonatomic) NSString * appCode;
        [Export("appCode")]
        NSString AppCode { get; }

        // @property (readonly, copy, nonatomic) NSString * appName;
        [Export("appName")]
        NSString AppName { get; }

        // @property (nonatomic, weak) NSObject<PushNotificationDelegate> * delegate;
        [NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
        PushNotificationDelegate Delegate { get; set; }

        // @property (assign, nonatomic) BOOL showPushnotificationAlert;
        [Export("showPushnotificationAlert")]
        bool ShowPushnotificationAlert { get; set; }

        // @property (readonly, copy, nonatomic) NSDictionary * launchNotification;
        [Export("launchNotification", ArgumentSemantic.Copy)]
        NSDictionary LaunchNotification { get; }

        // @property (readonly, nonatomic, strong) id<UNUserNotificationCenterDelegate> notificationCenterDelegate;
        [NullAllowed, Export("notificationCenterDelegate", ArgumentSemantic.Strong)]
        UNUserNotificationCenterDelegate NotificationCenterDelegate { get; }

        // +(void)initializeWithAppCode:(NSString *)appCode appName:(NSString *)appName;
        [Static]
        [Export("initializeWithAppCode:appName:")]
        void InitializeWithAppCode(NSString appCode, NSString appName);

        // +(PushNotificationManager *)pushManager;
        [Static]
        [Export("pushManager")]
        PushNotificationManager PushManager { get; }

        // -(void)registerForPushNotifications;
        [Export("registerForPushNotifications")]
        void RegisterForPushNotifications();

        // -(void)unregisterForPushNotifications;
        [Export("unregisterForPushNotifications")]
        void UnregisterForPushNotifications();

        // -(void)unregisterForPushNotificationsWithCompletion:(void (^)(NSError *))completion;
        [Export ("unregisterForPushNotificationsWithCompletion:")]
        void UnregisterForPushNotificationsWithCompletion (Action<NSError> completion);

        // -(instancetype)initWithApplicationCode:(NSString *)appCode appName:(NSString *)appName;
        [Export("initWithApplicationCode:appName:")]
        IntPtr Constructor(NSString appCode, NSString appName);

        // -(id)initWithApplicationCode:(NSString *)appCode navController:(UIViewController *)navController appName:(NSString *)appName __attribute__((deprecated("")));
        [Export("initWithApplicationCode:navController:appName:")]
        IntPtr Constructor(NSString appCode, UIViewController navController, NSString appName);

        // -(void)setTags:(NSDictionary *)tags;
        [Export("setTags:")]
        void SetTags(NSDictionary tags);

        // -(void)setTags:(NSDictionary *)tags withCompletion:(void (^)(NSError *))completion;
        [Export("setTags:withCompletion:")]
        void SetTags(NSDictionary tags, Action<NSError> completion);

        // -(void)loadTags;
        [Export("loadTags")]
        void LoadTags();

        // -(void)loadTags:(PushwooshGetTagsHandler)successHandler error:(PushwooshErrorHandler)errorHandler;
        [Export("loadTags:error:")]
        void LoadTags(PushwooshGetTagsHandler successHandler, PushwooshErrorHandler errorHandler);

        // -(void)sendAppOpen;
        [Export("sendAppOpen")]
        void SendAppOpen();

        // -(void)sendBadges:(NSInteger)badge;
        [Export("sendBadges:")]
        void SendBadges(nint badge);

        // -(void)sendSKPaymentTransactions:(NSArray *)transactions;
        [Export("sendSKPaymentTransactions:")]
        void SendSKPaymentTransactions(NSArray transactions);

        // -(void)sendPurchase:(NSString *)productIdentifier withPrice:(NSDecimalNumber *)price currencyCode:(NSString *)currencyCode andDate:(NSDate *)date;
        [Export("sendPurchase:withPrice:currencyCode:andDate:")]
        void SendPurchase(NSString productIdentifier, NSDecimalNumber price, NSString currencyCode, NSDate date);

        // -(NSString *)getPushToken;
        [Export("getPushToken")]
        NSString PushToken { get; }

        // -(NSString *)getHWID;
        [Export("getHWID")]
        NSString HWID { get; }

        // -(void)handlePushRegistration:(NSData *)devToken;
        [Export("handlePushRegistration:")]
        void HandlePushRegistration(NSData devToken);

        // -(void)handlePushRegistrationString:(NSString *)deviceID;
        [Export("handlePushRegistrationString:")]
        void HandlePushRegistrationString(NSString deviceID);

        // -(void)handlePushRegistrationFailure:(NSError *)error;
        [Export("handlePushRegistrationFailure:")]
        void HandlePushRegistrationFailure(NSError error);

        // -(BOOL)handlePushReceived:(NSDictionary *)userInfo;
        [Export("handlePushReceived:")]
        bool HandlePushReceived(NSDictionary userInfo);

        // -(NSDictionary *)getApnPayload:(NSDictionary *)pushNotification;
        [Export("getApnPayload:")]
        NSDictionary GetApnPayload(NSDictionary pushNotification);

        // -(NSString *)getCustomPushData:(NSDictionary *)pushNotification;
        [Export("getCustomPushData:")]
        NSString GetCustomPushData(NSDictionary pushNotification);

        // -(NSDictionary *)getCustomPushDataAsNSDict:(NSDictionary *)pushNotification;
        [Export("getCustomPushDataAsNSDict:")]
        NSDictionary GetCustomPushDataAsNSDict(NSDictionary pushNotification);

        // +(NSMutableDictionary *)getRemoteNotificationStatus;
        [Static]
        [Export("getRemoteNotificationStatus")]
        NSMutableDictionary RemoteNotificationStatus { get; }

        // +(void)clearNotificationCenter;
        [Static]
        [Export("clearNotificationCenter")]
        void ClearNotificationCenter();

        // -(void)setUserId:(NSString *)userId __attribute__((deprecated("")));
        [Export("setUserId:")]
        void SetUserId(NSString userId);

        // -(void)mergeUserId:(NSString *)oldUserId to:(NSString *)newUserId doMerge:(BOOL)doMerge completion:(void (^)(NSError *))completion __attribute__((deprecated("")));
        [Export("mergeUserId:to:doMerge:completion:")]
        void MergeUserId(NSString oldUserId, NSString newUserId, bool doMerge, Action<NSError> completion);

        // -(void)postEvent:(NSString *)event withAttributes:(NSDictionary *)attributes completion:(void (^)(NSError *))completion __attribute__((deprecated("")));
        [Export("postEvent:withAttributes:completion:")]
        void PostEvent(NSString @event, NSDictionary attributes, Action<NSError> completion);

        // -(void)postEvent:(NSString *)event withAttributes:(NSDictionary *)attributes __attribute__((deprecated("")));
        [Export("postEvent:withAttributes:")]
        void PostEvent(NSString @event, NSDictionary attributes);
    }

    // @protocol PWJavaScriptInterface
    [Model]
    [BaseType(typeof(NSObject))]
    public interface PWJavaScriptInterface
    {
        // @optional -(void)onWebViewStartLoad:(id)webView;
        [Export("onWebViewStartLoad:")]
        void OnWebViewStartLoad(UIWebView webView);

        // @optional -(void)onWebViewFinishLoad:(id)webView;
        [Export("onWebViewFinishLoad:")]
        void OnWebViewFinishLoad(UIWebView webView);

        // @optional -(void)onWebViewStartClose:(id)webView;
        [Export("onWebViewStartClose:")]
        void OnWebViewStartClose(UIWebView webView);
    }

    // @interface PWJavaScriptCallback : NSObject
    [BaseType(typeof(NSObject))]
    public interface PWJavaScriptCallback
    {
        // -(NSString *)execute;
        [Export("execute")]
        NSString Execute();

        // -(NSString *)executeWithParam:(NSString *)param;
        [Export("executeWithParam:")]
        NSString ExecuteWithParam(NSString param);

        // -(NSString *)executeWithParams:(NSArray *)params;
        [Export("executeWithParams:")]
        NSString ExecuteWithParams(NSArray @params);
    }

    // @interface PWInAppManager : NSObject
    [BaseType(typeof(NSObject))]
    public interface PWInAppManager
    {
        // +(instancetype)sharedManager;
        [Static]
        [Export("sharedManager")]
        PWInAppManager SharedManager { get; }

        // -(void)setUserId:(NSString *)userId;
        [Export("setUserId:")]
        void SetUserId(NSString userId);

        // -(void)mergeUserId:(NSString *)oldUserId to:(NSString *)newUserId doMerge:(BOOL)doMerge completion:(void (^)(NSError *))completion;
        [Export("mergeUserId:to:doMerge:completion:")]
        void MergeUserId(NSString oldUserId, NSString newUserId, bool doMerge, Action<NSError> completion);

        // -(void)postEvent:(NSString *)event withAttributes:(NSDictionary *)attributes completion:(void (^)(NSError *))completion;
        [Export("postEvent:withAttributes:completion:")]
        void PostEvent(NSString @event, NSDictionary attributes, Action<NSError> completion);

        // -(void)postEvent:(NSString *)event withAttributes:(NSDictionary *)attributes;
        [Export("postEvent:withAttributes:")]
        void PostEvent(NSString @event, NSDictionary attributes);

        // -(void)addJavascriptInterface:(NSObject<PWJavaScriptInterface> *)interface withName:(NSString *)name;
        [Export("addJavascriptInterface:withName:")]
        void AddJavascriptInterface(PWJavaScriptInterface @interface, NSString name);
    }

    // @interface PWInlineInAppView : UIView
    [BaseType(typeof(UIView))]
    public interface PWInlineInAppView
    {
        // @property (nonatomic) NSString * identifier;
        [Export("identifier")]
        string Identifier { get; set; }
    }


    // @protocol PWGeozonesDelegate <NSObject>
    [Model]
    [BaseType(typeof(NSObject))]
    interface PWGeozonesDelegate
    {
        // @optional -(void)didStartLocationTrackingWithManager:(PWGeozonesManager *)geozonesManager;
        [Export("didStartLocationTrackingWithManager:")]
        void DidStartLocationTrackingWithManager(PWGeozonesManager geozonesManager);

        // @optional -(void)geozonesManager:(PWGeozonesManager *)geozonesManager startingLocationTrackingDidFail:(NSError *)error;
        [Export("geozonesManager:startingLocationTrackingDidFail:")]
        void GeozonesManager(PWGeozonesManager geozonesManager, NSError error);

        // @optional -(void)geozonesManager:(PWGeozonesManager *)geozonesManager didSendLocation:(CLLocation *)location;
        [Export("geozonesManager:didSendLocation:")]
        void GeozonesManager(PWGeozonesManager geozonesManager, CLLocation location);
    }

    // @interface PWGeozonesManager : NSObject
    [BaseType(typeof(NSObject))]
    interface PWGeozonesManager
    {
        // @property (readonly, nonatomic) BOOL enabled;
        [Export("enabled")]
        bool Enabled { get; }

        [Wrap("WeakDelegate")]
        PWGeozonesDelegate Delegate { get; set; }

        // @property (nonatomic, weak) id<PWGeozonesDelegate> delegate;
        [NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
        NSObject WeakDelegate { get; set; }

        // +(instancetype)sharedManager;
        [Static]
        [Export("sharedManager")]
        PWGeozonesManager Instance { get; }

        // -(void)startLocationTracking;
        [Export("startLocationTracking")]
        void StartLocationTracking();

        // -(void)stopLocationTracking;
        [Export("stopLocationTracking")]
        void StopLocationTracking();

        // -(void)sendLocation:(CLLocation *)location;
        [Export("sendLocation:")]
        void SendLocation(CLLocation location);
    }
}



