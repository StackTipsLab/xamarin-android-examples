using System;
using System.Collections.Generic;

namespace JsonFeedParser
{
	public class RootObject
	{
		public string status { get; set; }
		public int count { get; set; }
		public int pages { get; set; }
		public List<Post> posts { get; set; }
	}

	public class Post
	{
		public int id { get; set; }
		public string url { get; set; }
		public string title { get; set; }
		public string content { get; set; }
		public string excerpt { get; set; }
		public string date { get; set; }
		public string thumbnail { get; set;}
	}
}