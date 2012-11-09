using System;
using System.Linq;

// Go here for more: http://msdn.microsoft.com/en-us/data/jj193542
namespace POCO_Haunt_Us
{
	class Program
	{
		static void Main(string[] args)
		{
			while(true)
				MainMenu();
		}

		private static void MainMenu()
		{
			using(var dataContext = new DataContext())
			{
				Console.Clear();
				Console.WriteLine("Available Blogs:");

				foreach(var blog in dataContext.Blogs)
					Console.WriteLine("{0}: {1}", blog.Id, blog.Name);

				Console.WriteLine("n: Create new blog");
				Console.WriteLine("------------------------");
				var selection = Console.ReadLine();

				if(selection == "n")
					CreateBlog(dataContext);
				else
				{
					int blogId;
					if(!Int32.TryParse(selection, out blogId) || !dataContext.Blogs.Where(b => b.Id == blogId).Any())
						return;

					// Lazy loading is not enabled by default
					BlogMenu(dataContext, dataContext.Blogs.Include("Posts").First(b => b.Id == blogId));
				}
			}
		}

		private static void BlogMenu(DataContext dataContext, Blog blog)
		{
			Console.Clear();
			Console.WriteLine("{0}", blog.Name);
			Console.WriteLine("n: Create new post");
			Console.WriteLine("v: View posts");
			Console.WriteLine("------------------------");
			var selection = Console.ReadLine();

			if(selection == "n")
				CreatePost(dataContext, blog);
			else if(selection == "v")
				DisplayBlog(dataContext, blog);
		}q

		private static void DisplayBlog(DataContext dataContext, Blog blog)
		{
			Console.Clear();
			var color = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine(blog.Name);

			Console.ForegroundColor = color;
			Console.WriteLine("    By {0}", blog.Author);
			Console.WriteLine(new String('-', 80));

			foreach(var post in blog.Posts)
			{
				Console.WriteLine("==== {0} ====", post.Title);
				Console.WriteLine(post.Content);
				Console.WriteLine("  - {0}, {1}. My life is just a {2}", blog.Author, post.Posted, post.AngstLevel);
				Console.WriteLine();
			}

			Console.WriteLine("Press enter to return to the menu");
			Console.ReadLine();
		}

		private static void CreateBlog(DataContext dataContext)
		{
			Console.Write("Blog name: ");
			var name = Console.ReadLine();

			Console.Write("Your name: ");
			var author = Console.ReadLine();

			dataContext.Blogs.Add(new Blog
			{
				Name = name,
				Author = author,
			});

			dataContext.SaveChanges();
		}

		private static void CreatePost(DataContext dataContext, Blog blog)
		{
			Console.Write("Title: ");
			var title = Console.ReadLine();

			Console.Write("Content: ");
			var content = Console.ReadLine();

			Console.Write("Angst Level: ");
			var angst = Console.ReadLine();

			blog.Posts.Add(new Post
			{
				Title = title,
				Content = content,
				AngstLevel = angst,
				Posted = DateTime.Now,
			});

			dataContext.SaveChanges();
		}
	}
}
