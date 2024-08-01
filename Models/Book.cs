using System.ComponentModel.DataAnnotations;

namespace LibraryCenter.Models
{
	public class Book
	{
		[Key]
		public int Id { get; set; }

		[Display(Name = "זאנר")]

		public string? Genre { get; set; }

		[Display(Name = "גובה")]
		public double Height { get; set; }

		[Display(Name = "כותרת")]
		public string? Title { get; set; }

		public Shelf? shelf { get; set; }

		public Library?  library { get; set; }
		


	}
}
