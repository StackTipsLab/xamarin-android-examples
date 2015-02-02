using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Webkit;

namespace WebViewExample
{
	[Activity (Label = "WebViewExample", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			WebView mWebView = FindViewById<WebView>(Resource.Id.webView);

			mWebView.Settings.JavaScriptEnabled = true;

			//
			mWebView.SetWebViewClient (new MyWebViewClient());

			//Load url to be randered on WebView
			mWebView.LoadUrl("http://www.javatechig.com");
		}

		public class MyWebViewClient : WebViewClient
		{
		  public override bool ShouldOverrideUrlLoading (WebView view, string url)
			{
				view.LoadUrl (url);
				return true;
			}

			public override void OnPageStarted (WebView view, string url, Android.Graphics.Bitmap favicon)
			{
				base.OnPageStarted (view, url, favicon);
			}

			public override void OnPageFinished (WebView view, string url)
			{
				base.OnPageFinished (view, url);
			}

			public override void OnReceivedError (WebView view, ClientError errorCode, string description, string failingUrl)
			{
				base.OnReceivedError (view, errorCode, description, failingUrl);
			}
		}
	}
}


