using System;
using System.Data.Entity;

namespace POCO_Haunt_Us
{
	class DataContext : DbContext
	{
		public DbSet<Blog> Blogs { get; set; }
		public DbSet<Post> Posts { get; set; }
	}
}
