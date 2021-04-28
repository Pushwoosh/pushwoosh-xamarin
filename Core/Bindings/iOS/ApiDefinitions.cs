using System;
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

        // @property (nonatomic) NSString *language;
        [NullAllowed, Export("language")]
        NSString Language { get; set; }

        // @property (readonly, copy, nonatomic) NSDictionary * launchNotification;
        [Export("launchNotification", ArgumentSemantic.Copy)]
        NSDictionary LaunchNotification { get; }

        // @property (readonly, nonatomic, strong) id<UNUserNotificationCenterDelegate> notificationCenterDelegate;
        [NullAllowed, Export("notificationCenterDelegate", ArgumentSemantic.Strong)]
        IUNUserNotificationCenterDelegate NotificationCenterDelegate { get; }

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

    // @protocol PWInboxMessageProtocol <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface PWInboxMessageProtocol : INativeObject
    {
        // @required @property (readonly, nonatomic) NSString * code;
        [Abstract]
        [Export("code")]
        string Code { get; }

        // @required @property (readonly, nonatomic) NSString * title;
        [Abstract]
        [Export("title")]
        string Title { get; }

        // @required @property (readonly, nonatomic) NSString * imageUrl;
        [Abstract]
        [Export("imageUrl")]
        string ImageUrl { get; }

        // @required @property (readonly, nonatomic) NSString * message;
        [Abstract]
        [Export("message")]
        string Message { get; }

        // @required @property (readonly, nonatomic) NSDate * sendDate;
        [Abstract]
        [Export("sendDate")]
        NSDate SendDate { get; }

        // @required @property (readonly, nonatomic) PWInboxMessageType type;
        [Abstract]
        [Export("type")]
        PWInboxMessageType Type { get; }

        // @required @property (readonly, nonatomic) BOOL isRead;
        [Abstract]
        [Export("isRead")]
        bool IsRead { get; }

        // @required @property (readonly, nonatomic) BOOL isActionPerformed;
        [Abstract]
        [Export("isActionPerformed")]
        bool IsActionPerformed { get; }

        // @required @property (readonly, nonatomic) NSString * attachmentUrl;
        [Abstract]
        [Export("attachmentUrl")]
        string AttachmentUrl { get; }

        // @required @property (readonly, nonatomic) NSDictionary * actionParams;
        [Abstract]
        [Export("actionParams")]
        NSDictionary ActionParams { get; }
    }

    // @interface PWInbox : NSObject
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    interface PWInbox : INativeObject
    {
        // extern NSString *const PWInboxMessagesDidUpdateNotification;
        [Field("PWInboxMessagesDidUpdateNotification", "__Internal")]
        NSString PWInboxMessagesDidUpdateNotification { get; }

        // extern NSString *const PWInboxMessagesDidReceiveInPushNotification;
        [Field("PWInboxMessagesDidReceiveInPushNotification", "__Internal")]
        NSString PWInboxMessagesDidReceiveInPushNotification { get; }

        // +(void)messagesWithNoActionPerformedCountWithCompletion:(void (^)(NSInteger, NSError *))completion;
        [Static]
        [Export("messagesWithNoActionPerformedCountWithCompletion:")]
        void MessagesWithNoActionPerformedCountWithCompletion(Action<nint, NSError> completion);

        // +(void)unreadMessagesCountWithCompletion:(void (^)(NSInteger, NSError *))completion;
        [Static]
        [Export("unreadMessagesCountWithCompletion:")]
        void UnreadMessagesCountWithCompletion(Action<nint, NSError> completion);

        // +(void)messagesCountWithCompletion:(void (^)(NSInteger, NSError *))completion;
        [Static]
        [Export("messagesCountWithCompletion:")]
        void MessagesCountWithCompletion(Action<nint, NSError> completion);

        // +(void)loadMessagesWithCompletion:(void (^)(NSArray<NSObject<PWInboxMessageProtocol> *> *, NSError *))completion;
        [Static]
        [Export("loadMessagesWithCompletion:")]
        void LoadMessagesWithCompletion(Action<NSArray<PWInboxMessageProtocol>, NSError> completion);

        // +(void)performActionForMessageWithCode:(NSString *)code;
        [Static]
        [Export("performActionForMessageWithCode:")]
        void PerformActionForMessageWithCode(string code);

        // +(void)deleteMessagesWithCodes:(NSArray<NSString *> *)codes;
        [Static]
        [Export("deleteMessagesWithCodes:")]
        void DeleteMessagesWithCodes(string[] codes);

        // +(void)readMessagesWithCodes:(NSArray<NSString *> *)codes;
        [Static]
        [Export("readMessagesWithCodes:")]
        void ReadMessagesWithCodes(string[] codes);

        // +(id<NSObject>)addObserverForDidReceiveInPushNotificationCompletion:(void (^)(NSArray<NSObject<PWInboxMessageProtocol> *> *))completion;
        [Static]
        [Export("addObserverForDidReceiveInPushNotificationCompletion:")]
        NSObject AddObserverForDidReceiveInPushNotificationCompletion(Action<NSArray<PWInboxMessageProtocol>> completion);

        // +(id<NSObject>)addObserverForUpdateInboxMessagesCompletion:(void (^)(NSArray<NSObject<PWInboxMessageProtocol> *> *, NSArray<NSObject<PWInboxMessageProtocol> *> *, NSArray<NSObject<PWInboxMessageProtocol> *> *))completion;
        [Static]
        [Export("addObserverForUpdateInboxMessagesCompletion:")]
        NSObject AddObserverForUpdateInboxMessagesCompletion(Action<NSArray<PWInboxMessageProtocol>, NSArray<PWInboxMessageProtocol>, NSArray<PWInboxMessageProtocol>> completion);

        //+ (id<NSObject>) addObserverForUnreadMessagesCountUsingBlock:(void (^)(NSUInteger count))block;
        [Static]
        [Export("addObserverForUnreadMessagesCountUsingBlock:")]
        NSObject AddObserverForUnreadMessagesCount(Action<nint> completion);

        // +(void)removeObserver:(id<NSObject>)observer;
        [Static]
        [Export("removeObserver:")]
        void RemoveObserver(NSObject observer);
    }

    [Native]
    public enum PWInboxMessageType : long
    {
        Plain = 0,
        Richmedia = 1,
        Url = 2,
        Deeplink = 3
    }
}