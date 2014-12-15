using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SQLite;

namespace SQLiteORMExample
{	
	class Inventory
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		[NotNull]
		public int ItemCode { get; set;}
	
		public String Name{ get; set;}
		public String Category { get; set;}
		public double Price{ get; set;}
		public bool StockAvailable { get; set;}
	}
}