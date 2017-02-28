using System;
using Foundation;
using UIKit;
using Pushwoosh;

namespace PushwooshSample
{
	public class JavaScriptInterface : NSObject
	{
		[Export("onWebViewStartLoad:")]
		void onWebViewStartLoad(UIWebView webView)
		{
			Console.WriteLine("onWebViewStartLoad");
		}

		[Export("onWebViewFinishLoad:")]
		void onWebViewFinishLoad(UIWebView webView)
		{
			Console.WriteLine("onWebViewFinishLoad");
		}

		[Export("onWebViewStartClose:")]
		void onWebViewStartClose(UIWebView webView)
		{
			Console.WriteLine("onWebViewStartClose");
		}

		[Export("test::")]
		public void test(NSString str, PWJavaScriptCallback callback)
		{
			Console.WriteLine("JavaScript interface test called");

			callback.ExecuteWithParam(str);
		}

		[Export("log:")]
		public void log(NSString str)
		{
			Console.WriteLine(str);
		}
	}
}
