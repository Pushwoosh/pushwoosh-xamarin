using System;

using ObjCRuntime;
using Foundation;
using UIKit;

namespace Pushwoosh.Inbox
{
    [Native]
    public enum PWInboxMessageType : long
    {
        Plain = 0,
        Richmedia = 1,
        Url = 2,
        Deeplink = 3
    }

    // typedef NSString * (^PWIDateFormatterBlock)(NSDate *, NSObject *);
    delegate string PWIDateFormatterBlock(NSDate arg0, NSObject arg1);

    // @interface PWIInboxStyle : NSObject
    [BaseType(typeof(NSObject))]
    interface PWIInboxStyle
    {
        // @property (readwrite, nonatomic) PWIDateFormatterBlock dateFormatterBlock;
        [Export("dateFormatterBlock", ArgumentSemantic.Assign)]
        PWIDateFormatterBlock DateFormatterBlock { get; set; }

        // @property (readwrite, nonatomic) UIImage * defaultImageIcon;
        [Export("defaultImageIcon", ArgumentSemantic.Assign)]
        UIImage DefaultImageIcon { get; set; }

        // @property (readwrite, nonatomic) UIFont * defaultFont;
        [Export("defaultFont", ArgumentSemantic.Assign)]
        UIFont DefaultFont { get; set; }

        // @property (readwrite, nonatomic) UIColor * defaultTextColor;
        [Export("defaultTextColor", ArgumentSemantic.Assign)]
        UIColor DefaultTextColor { get; set; }

        // @property (readwrite, nonatomic) UIColor * backgroundColor;
        [Export("backgroundColor", ArgumentSemantic.Assign)]
        UIColor BackgroundColor { get; set; }

        // @property (readwrite, nonatomic) UIColor * selectionColor;
        [Export("selectionColor", ArgumentSemantic.Assign)]
        UIColor SelectionColor { get; set; }

        // @property (readwrite, nonatomic) UIImage * unreadImage;
        [Export("unreadImage", ArgumentSemantic.Assign)]
        UIImage UnreadImage { get; set; }

        // @property (readwrite, nonatomic) UIImage * listErrorImage;
        [Export("listErrorImage", ArgumentSemantic.Assign)]
        UIImage ListErrorImage { get; set; }

        // @property (readwrite, nonatomic) NSString * listErrorMessage;
        [Export("listErrorMessage")]
        string ListErrorMessage { get; set; }

        // @property (readwrite, nonatomic) UIImage * listEmptyImage;
        [Export("listEmptyImage", ArgumentSemantic.Assign)]
        UIImage ListEmptyImage { get; set; }

        // @property (readwrite, nonatomic) NSString * listEmptyMessage;
        [Export("listEmptyMessage")]
        string ListEmptyMessage { get; set; }

        // @property (readwrite, nonatomic) UIColor * accentColor;
        [Export("accentColor", ArgumentSemantic.Assign)]
        UIColor AccentColor { get; set; }

        // @property (readwrite, nonatomic) UIColor * titleColor;
        [Export("titleColor", ArgumentSemantic.Assign)]
        UIColor TitleColor { get; set; }

        // @property (readwrite, nonatomic) UIColor * descriptionColor;
        [Export("descriptionColor", ArgumentSemantic.Assign)]
        UIColor DescriptionColor { get; set; }

        // @property (readwrite, nonatomic) UIColor * dateColor;
        [Export("dateColor", ArgumentSemantic.Assign)]
        UIColor DateColor { get; set; }

        // @property (readwrite, nonatomic) UIColor * separatorColor;
        [Export("separatorColor", ArgumentSemantic.Assign)]
        UIColor SeparatorColor { get; set; }

        // @property (readwrite, nonatomic) UIFont * titleFont;
        [Export("titleFont", ArgumentSemantic.Assign)]
        UIFont TitleFont { get; set; }

        // @property (readwrite, nonatomic) UIFont * descriptionFont;
        [Export("descriptionFont", ArgumentSemantic.Assign)]
        UIFont DescriptionFont { get; set; }

        // @property (readwrite, nonatomic) UIFont * dateFont;
        [Export("dateFont", ArgumentSemantic.Assign)]
        UIFont DateFont { get; set; }

        // @property (readwrite, nonatomic) UIColor * barBackgroundColor;
        [Export("barBackgroundColor", ArgumentSemantic.Assign)]
        UIColor BarBackgroundColor { get; set; }

        // @property (readwrite, nonatomic) UIColor * barAccentColor;
        [Export("barAccentColor", ArgumentSemantic.Assign)]
        UIColor BarAccentColor { get; set; }

        // @property (readwrite, nonatomic) UIColor * barTextColor;
        [Export("barTextColor", ArgumentSemantic.Assign)]
        UIColor BarTextColor { get; set; }

        // @property (readwrite, nonatomic) NSString * barTitle;
        [Export("barTitle")]
        string BarTitle { get; set; }

        // +(instancetype)defaultStyle;
        [Static]
        [Export("defaultStyle")]
        PWIInboxStyle DefaultStyle();

        // +(void)setupDefaultStyle:(PWIInboxStyle *)style;
        [Static]
        [Export("setupDefaultStyle:")]
        void SetupDefaultStyle(PWIInboxStyle style);

        // +(instancetype)customStyleWithDefaultImageIcon:(UIImage *)icon textColor:(UIColor *)textColor accentColor:(UIColor *)accentColor font:(UIFont *)font;
        [Static]
        [Export("customStyleWithDefaultImageIcon:textColor:accentColor:font:")]
        PWIInboxStyle CustomStyleWithDefaultImageIcon(UIImage icon, UIColor textColor, UIColor accentColor, UIFont font);

        // +(instancetype)customStyleWithDefaultImageIcon:(UIImage *)icon textColor:(UIColor *)textColor accentColor:(UIColor *)accentColor font:(UIFont *)font dateFromatterBlock:(PWIDateFormatterBlock)dateFormatterBlock;
        [Static]
        [Export("customStyleWithDefaultImageIcon:textColor:accentColor:font:dateFromatterBlock:")]
        PWIInboxStyle CustomStyleWithDefaultImageIcon(UIImage icon, UIColor textColor, UIColor accentColor, UIFont font, PWIDateFormatterBlock dateFormatterBlock);
    }

    // @interface PWIInboxViewController : UIViewController
    [BaseType(typeof(UIViewController))]
    [DisableDefaultCtor]
    interface PWIInboxViewController
    {
        // @property (nonatomic) void (^onMessageClickBlock)(id<PWInboxMessageProtocol>);
        [Export ("onMessageClickBlock", ArgumentSemantic.Assign)]
        Action<IPWInboxMessageProtocol> OnMessageClickBlock { get; set; }

        // -(void)reloadData;
        [Export("reloadData")]
        void ReloadData();
    }

    // @interface PWIInboxUI : NSObject
    [BaseType(typeof(NSObject))]
    interface PWIInboxUI
    {
        // +(PWIInboxViewController *)createInboxControllerWithStyle:(PWIInboxStyle *)style;
        [Static]
        [Export("createInboxControllerWithStyle:")]
        PWIInboxViewController CreateInboxControllerWithStyle(PWIInboxStyle style);
    }
}