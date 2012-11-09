using System;

namespace POCO_Haunt_Us
{
	class Post
	{
		public int Id 
		{ get; set; }

		public string Title
		{ get; set; }

		public string Content
		{ get; set; }

		public string AngstLevel
		{ get; set; }

		public DateTime Posted
		{ get; set; }

		public int BlogId 
		{ get; set; }

		public virtual Blog Blog 
		{ get; set; }
	}
}
