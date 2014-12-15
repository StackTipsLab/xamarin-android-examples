using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace ImageViewExample
{
	[Activity (Label = "ImageViewExample", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

		Button downloadButton;
		ImageView imageView;
		LinearLayout progressLayout;

		//Instance of webclient for async processing
		WebClient webClient;

		protected override void OnCreate (Bundle bundle){
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			this.imageView = FindViewById<ImageView> (Resource.Id.imageView1);

			//Hide progressbar initially
			this.progressLayout = FindViewById<LinearLayout> (Resource.Id.progressLayout);
			this.progressLayout.Visibility = ViewStates.Gone;

			// Get views from the layout resource axml file
			this.downloadButton = FindViewById<Button> (Resource.Id.downloadButton);
			downloadButton.Click += downloadAsync;
		}

		async void downloadAsync(object sender, System.EventArgs ea){
			webClient = new WebClient ();
			var url = new Uri ("http://doubletreebyhiltonsanjose.com/wp-content/uploads/2014/08/Dog-Pictures-1024x698.jpg");
			byte[] imageBytes = null;

			//Show loading progress
			this.progressLayout.Visibility = ViewStates.Visible;

			//Toggle button click listener to cancel the task
			this.downloadButton.Text = "Cancel Download";
			this.downloadButton.Click -= downloadAsync;
			this.downloadButton.Click += cancelDownload;

			try{
				imageBytes = await webClient.DownloadDataTaskAsync(url);
			} catch(TaskCanceledException){
				this.progressLayout.Visibility = ViewStates.Gone;
				return;
			} catch(Exception e){
				this.progressLayout.Visibility = ViewStates.Gone;

				this.downloadButton.Click -= cancelDownload;
				this.downloadButton.Click += downloadAsync;
				this.downloadButton.Text = "Download Image";
				return;
			}

			//Saving bitmap locally
			string documentsPath = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);	
			string localFilename = "image.png";
			string localPath = System.IO.Path.Combine (documentsPath, localFilename);

			//Save the Image using writeAsync
			FileStream fs = new FileStream (localPath, FileMode.OpenOrCreate);
			await fs.WriteAsync (imageBytes, 0, imageBytes.Length);

			Console.WriteLine("Saving image in local path: "+localPath);

			//Close file connection
			fs.Close ();

			BitmapFactory.Options options = new BitmapFactory.Options ();
			options.InJustDecodeBounds = true;
			await BitmapFactory.DecodeFileAsync (localPath, options);

			//Resizing bitmap image
			options.InSampleSize = options.OutWidth > options.OutHeight ? options.OutHeight / imageView.Height : options.OutWidth / imageView.Width;
			options.InJustDecodeBounds = false;

			Bitmap bitmap = await BitmapFactory.DecodeFileAsync (localPath, options);
			imageView.SetImageBitmap (bitmap);

			//Hide progressbar layout
			this.progressLayout.Visibility = ViewStates.Gone;

			//Toggle button click listener
			this.downloadButton.Click -= cancelDownload;
			this.downloadButton.Click += downloadAsync;
			this.downloadButton.Text = "Download Image";
		}

		void cancelDownload(object sender, System.EventArgs ea){
			if(webClient!=null)
				webClient.CancelAsync ();

			//Hide progressbar layout
			this.progressLayout.Visibility = ViewStates.Gone;

			//Toggle button click listener
			this.downloadButton.Click -= cancelDownload;
			this.downloadButton.Click += downloadAsync;
			this.downloadButton.Text = "Download Image";
		}
	}
}


