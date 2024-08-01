using System.ComponentModel.DataAnnotations;

namespace LibraryCenter.Models
{
	public class Library
	{

		[Key]
		public int Id { get; set; }

		[Display(Name = "זאנר")]
		public string? Genre { get; set; }

		public List<Shelf>? shelves { get; set; }



	}
}	
