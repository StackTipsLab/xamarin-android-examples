using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace SQLiteORMExample
{
	[Activity (Label = "Inventory List", MainLauncher = true, Icon = "@drawable/ic_launcher")]
	public class MainActivity : Activity
	{
		ListView listView;
		InventoryListAdapter adapter;

		List<Inventory>  listData = new List<Inventory>();

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.ListActivity);

			//Initializing listview
			listView = FindViewById<ListView> (Resource.Id.IntentoryList);
			listView.ItemClick += OnListItemClick;
		}

		protected override void OnResume ()
		{
			base.OnResume ();

			//Getting instance of SQLiteConnection class
			//Will be used for all SQL transaction
			var db = DBHandler.GetInstance.GetSQLiteConnection;
			//Create table if doesnt exist already
			db.CreateTable<Inventory> ();

			//Fetching records from 'Inventory' table
			IEnumerable<Inventory> obj =  db.Table<Inventory> ();

			//Converting to listview
			listData = obj.ToList();

			adapter = new InventoryListAdapter(this, listData);

			//Setting Adapter to ListView
			listView.Adapter = adapter;

			Console.WriteLine ("Reading data");
			var table = db.Table<Inventory> ();
			foreach (var s in table) {
				Console.WriteLine (s.ItemCode + " " + s.Name);
			}
		}

		void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			//Geting object from the selected row
			Inventory newInventory = listData.ElementAt (e.Position);

			//Passing object form one activity to other
			Intent i = new Intent(Application.Context, typeof(ManageInventoryActivity));
			i.PutExtra("item", JsonConvert.SerializeObject(newInventory));
			StartActivity(i);

		}


		public override bool OnCreateOptionsMenu (IMenu menu){
			MenuInflater.Inflate (Resource.Menu.MainMenu, menu);       
			return true;
		}

		public override bool OnOptionsItemSelected(IMenuItem item){
			switch (item.ItemId)
			{
			case Resource.Id.AddInventory:
				StartActivity (typeof(AddInventoryActivity));
				return true;
			default:
				return base.OnOptionsItemSelected(item);
			}
		}
	}
}