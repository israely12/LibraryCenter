using System.ComponentModel.DataAnnotations;

namespace LibraryCenter.Models
{
	public class Shelf
	{
		[Key]
		public int Id { get; set; }

		[Display(Name = "גובה")]
		public double Height { get; set; }

		public List<Book>? Books { get; set; }

		public Library? Library { get; set; }
	}
}
