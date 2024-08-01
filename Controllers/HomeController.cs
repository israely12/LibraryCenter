using LibraryCenter.DAL;
using LibraryCenter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;

namespace LibraryCenter.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Libraries()
		{
			List<Library> Libraries = Data.Get.libraries.ToList();
			return View(Libraries);

		}


		public IActionResult Create() 
		{
			return View(new Library()); 
		}



		public IActionResult AddNewLibrary(Library library)
		{
			Data.Get.libraries.Add(library);
			Data.Get.SaveChanges();

			return RedirectToAction("Libraries");

		}



        public IActionResult Edit(int? id)
        {


            if (id == null)
            {
                return RedirectToAction("Libraries");
            }

            Library? library = Data.Get.libraries.FirstOrDefault(lib => lib.Id == id);

            if (library == null)
            {
                return RedirectToAction("Libraries");
            }
			
            return View(library);
         
        }


		[HttpPost, ValidateAntiForgeryToken]
		public IActionResult EditLibrarySaved(Library newLibrary)
        {
            if (newLibrary == null)
            {
                return RedirectToAction("Libraries");
            }
            Library? existingLibrary = Data.Get.libraries.FirstOrDefault(lib => lib.Id == newLibrary.Id);
            if (existingLibrary == null)
            {
                return RedirectToAction("Libraries");
            }

            Data.Get.Entry(existingLibrary).CurrentValues.SetValues(newLibrary);
            Data.Get.SaveChanges();
            return RedirectToAction("Libraries");

        }



		public IActionResult DeleteLibrary(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			List<Library> libraryList = Data.Get.libraries.ToList();

			Library? libraryToRemove = libraryList.Find(lib => lib.Id == id);
			if (libraryToRemove == null)
			{
				return NotFound();
			}
			Data.Get.libraries.Remove(libraryToRemove);
			Data.Get.SaveChanges();

			return RedirectToAction(nameof(Libraries));
		}


        [HttpGet]
        public IActionResult LibraryShelves(int id)
        {
            List<Shelf> shelves = Data.Get.shelves
                .Include(s => s.Library)
                .Where(s => s.Library.Id == id)
                .ToList();

            return View(shelves);
        }


        public IActionResult Shelves()
        {
            List<Shelf> Shelves = Data.Get.shelves.ToList();
            return View(Shelves);

        }
        public IActionResult CreataShelf()
        {
            return View(new Shelf());
        }



        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddNewShelf(Shelf shelf ,int LibraryId)
        {
            Library? library = Data.Get.libraries.FirstOrDefault(l => l.Id == LibraryId);

            if (library.shelves == null)
            {
                library.shelves = new List<Shelf>();
            }

            library.shelves.Add(shelf);
            Data.Get.shelves.Add(shelf);

           
            Data.Get.SaveChanges();

           
            
            return RedirectToAction("LibraryShelves", new { id = LibraryId });
        }
        [HttpGet]
        public IActionResult BooksShelf(int? id)
        {
            List<Book> books = Data.Get.books
               .Include(b => b.shelf)
               .Where(b => b.shelf.Id == id)
               .ToList();

            return View(books);
        }
      
        public IActionResult CreateNewBook()
        {
            return View(new Book());
        }


        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddNewBook(Book book, int ShelfId)
        {
            Shelf? shelf = Data.Get.shelves.FirstOrDefault(s => s.Id == ShelfId);

            if (shelf.Books == null)
            {
                shelf.Books = new List<Book>();
            }


            double BookHeight = book.Height;
            double Shelfheight = shelf.Height;

            if (BookHeight > Shelfheight)
            {
                return OverString();

            }
            if (BookHeight < (Shelfheight - 10))
            {
                shelf.Books.Add(book);
                Data.Get.books.Add(book);
                Data.Get.SaveChanges();
               

                return UnderString();   
            }

            shelf.Books.Add(book);
            Data.Get.books.Add(book);
            Data.Get.SaveChanges();
            return RedirectToAction("BooksShelf", new { id = ShelfId });
        }
        public IActionResult AllBooks()
        {
            List<Book> Books = Data.Get.books.ToList();
            return View(Books);

        }

        public IActionResult OverString()
        {
            return Content("הגובה של הספר אינו תואם את גובה המדף");
        }

        public IActionResult UnderString()
        {
            return Content("הגובה של הספר נמוך משמעותית מגובה המדף");
        }


       
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Privacy()
		{
			return View();
		}



		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
      

    }
}
