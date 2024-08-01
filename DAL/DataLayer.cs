using LibraryCenter.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
namespace LibraryCenter.DAL
{
	public class DataLayer : DbContext
	{

		public DataLayer(string connectionString) : base(GetOptions(connectionString))
		{
			Database.EnsureCreated();

			
		}
		public DbSet<Library> libraries { get; set; }

		public DbSet<Shelf> shelves { get; set; }

		public DbSet<Book> books { get; set; }

		
		private static DbContextOptions GetOptions(string connectionString)
		{
			return new DbContextOptionsBuilder()
				.UseSqlServer(connectionString)
				.Options;
		}
	}
}
