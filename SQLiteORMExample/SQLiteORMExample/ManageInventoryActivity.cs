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
using SQLite;
using Newtonsoft.Json;

namespace SQLiteORMExample
{
	[Activity (Label = "Manage Item", Icon = "@drawable/ic_launcher")]			
	public class ManageInventoryActivity : Activity
	{
		EditText ItemCode;
		EditText ItemName;
		EditText ItemPrice;
		EditText ItemCategory;
		ToggleButton IsStockAvailable;

		Inventory item ;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.ManageInventoryActivity);

			//Show back butotn on actionbar,
			ActionBar.SetDisplayHomeAsUpEnabled (true);

			//Retrieve data bundle passed from ListActivity
			item = JsonConvert.DeserializeObject<Inventory>(Intent.GetStringExtra("item"));

			//Initializing objects
			ItemCode = FindViewById<EditText> (Resource.Id.ItemCode);
			ItemName = FindViewById<EditText> (Resource.Id.ItemName);
			ItemCategory = FindViewById<EditText> (Resource.Id.ItemCategory);
			ItemPrice = FindViewById<EditText> (Resource.Id.ItemPrice);
			IsStockAvailable = FindViewById<ToggleButton> (Resource.Id.IsStockAvailable);

			ItemCode.Text = item.ItemCode.ToString();
			ItemName.Text = item.Name;
			ItemCategory.Text = item.Category;
			ItemPrice.Text = item.Price.ToString();
			IsStockAvailable.Checked = item.StockAvailable;


			//Delete inventory
			Button DeleteButton = FindViewById<Button> (Resource.Id.Delete);
			DeleteButton.Click += Delete;

			//Update inventory
			Button UpdateButton = FindViewById<Button> (Resource.Id.Update);
			UpdateButton.Click += Update;
		}

		public void Delete(object sender, EventArgs e){
			var db = DBHandler.GetInstance.GetSQLiteConnection;
			db.Delete (item);

			Android.Widget.Toast.MakeText(this, "Item deleted", Android.Widget.ToastLength.Short).Show();
		}

		public void Update(object sender, EventArgs e){

			var db = DBHandler.GetInstance.GetSQLiteConnection;

			//Create table if doesnt exist already
			db.CreateTable<Inventory> ();

			//Creating inventory object for storing into db
			item.ItemCode = Convert.ToInt32(ItemCode.Text);
			item.Name = ItemName.Text;
			item.Category = ItemCategory.Text;
			item.Price = Convert.ToDouble(ItemPrice.Text);
			item.StockAvailable = IsStockAvailable.Checked;

			//Inserting record into database
			db.Update (item); 

			Android.Widget.Toast.MakeText(this, "Item updated", Android.Widget.ToastLength.Short).Show();
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