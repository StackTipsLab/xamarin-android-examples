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

namespace SQLiteORMExample
{
	[Activity (Label = "Add Item", Icon = "@drawable/ic_launcher")]			
	public class AddInventoryActivity : Activity
	{
		EditText ItemCode;
		EditText ItemName;
		EditText ItemPrice;
		EditText ItemCategory;
		ToggleButton IsStockAvailable;


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.AddInventoryActivity);

			/*
			 * Show back butotn on actionbar,
			 * Clicking back icon, activity closes
			 */ 
			ActionBar.SetDisplayHomeAsUpEnabled (true);
		
			//Initializing objects
			ItemCode = FindViewById<EditText> (Resource.Id.ItemCode);
			ItemName = FindViewById<EditText> (Resource.Id.ItemName);
			ItemCategory = FindViewById<EditText> (Resource.Id.ItemCategory);
			ItemPrice = FindViewById<EditText> (Resource.Id.ItemPrice);
			IsStockAvailable = FindViewById<ToggleButton> (Resource.Id.IsStockAvailable);

			Button AddInventory = FindViewById<Button> (Resource.Id.AddItem);
			AddInventory.Click += SaveInventory;
		}

		public void SaveInventory(object sender, EventArgs e){

			//Getting instance of SQLiteConnection class
			//Will be used for all SQL transaction
			var db = DBHandler.GetInstance.GetSQLiteConnection;

			//Create table if doesnt exist already
			db.CreateTable<Inventory> ();

			//Creating inventory object for storing into db
			Inventory newInventory = new Inventory ();
			newInventory.ItemCode = Convert.ToInt32(ItemCode.Text);
			newInventory.Name = ItemName.Text;
			newInventory.Category = ItemCategory.Text;
			newInventory.Price = Convert.ToDouble(ItemPrice.Text);
			newInventory.StockAvailable = IsStockAvailable.Checked;

			//Inserting record into database
			db.Insert (newInventory); 

			//Display toast message to user
			Android.Widget.Toast.MakeText(this, "Item added to inventory", 
				Android.Widget.ToastLength.Short).Show();
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