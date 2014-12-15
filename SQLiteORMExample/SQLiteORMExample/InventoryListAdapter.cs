using System;
using Android.App;
using Android.Widget;
using Android.Views;
using System.Collections.Generic;

namespace SQLiteORMExample
{
	class InventoryListAdapter : BaseAdapter<Inventory>
	{
		List<Inventory> listData;

		Activity context;

		public InventoryListAdapter (Activity activity, List<Inventory> items)  
			: base()
		{
			this.context = activity;
			this.listData = items;
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override int Count {
			get { return listData.Count; }
		}


		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View view = convertView; 

			// re-use an existing view, if one is available
			// otherwise create a new one
			if (view == null)
				view = context.LayoutInflater.Inflate (Resource.Layout.ListItem, parent, false);
				
			var text = "Code=" + listData [position].ItemCode + "    Name=" + listData [position].Name + "    Price:" + listData [position].Price + "    Category=" + listData [position].Category +
				"    IsStockAvailable=" + listData [position].StockAvailable;

			view.FindViewById<TextView>(Resource.Id.textView1).Text = text;

			return view;
		}

		public override Inventory this[int index] {
			get { return listData[index]; }
		}
	}
}

