
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Android.Text;

namespace JsonFeedParser
{
	[Activity (Label = "Feed Details", Icon = "@drawable/icon")]			
	public class FeedDetailsActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.FeedDetailsActivity);

			//Show back butotn on actionbar,
			ActionBar.SetDisplayHomeAsUpEnabled (true);

			//Retrieve data bundle passed from ListActivity
			Post item = JsonConvert.DeserializeObject<Post>(Intent.GetStringExtra("item"));

			FindViewById<TextView> (Resource.Id.FeedTitle).Text = Html.FromHtml (item.title).ToString();
			FindViewById<TextView> (Resource.Id.FeedContent).Text = Html.FromHtml (item.content).ToString();

			ImageView imageView = FindViewById<ImageView> (Resource.Id.FeaturedImg);

			//Download and display image
			Koush.UrlImageViewHelper.SetUrlDrawable (imageView, Html.FromHtml (item.thumbnail).ToString(), Resource.Drawable.Placeholder);
		}

		//Handling home button click event
		public override bool OnOptionsItemSelected(IMenuItem item){
			switch (item.ItemId) {
			case Android.Resource.Id.Home:
				Finish();
				break;
			}
			return true;
		}
	}
}

