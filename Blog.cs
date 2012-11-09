using System;
using System.Collections.Generic;

namespace POCO_Haunt_Us
{
	class Blog
	{
		public int Id
		{ get; set; }

		public string Name
		{ get; set; }

		public string Author
		{ get; set; }

		public virtual ICollection<Post> Posts
		{ get; set; }
	}
}
