using System;
using SQLite;

namespace SQLiteORMExample
{
	public class DBHandler
	{
		private const string DATABASE_NAME = "Intentory_DB.db";

		string path = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);

		private static SQLiteConnection _connection;

		private static DBHandler instance;

		/**
		 * Private Constructor to restrict, 
		 * instantiation outside class
		 */ 
		private DBHandler(){}


		public static DBHandler GetInstance {
			get{ 
				if (null == instance)
					instance = new DBHandler ();
				return instance;
			   }
		}


		public SQLiteConnection GetSQLiteConnection {
			get{ 
				if (null == _connection)
					_connection = new SQLiteConnection (System.IO.Path.Combine (path, DATABASE_NAME));

				return _connection;
			}
		}
	}
}