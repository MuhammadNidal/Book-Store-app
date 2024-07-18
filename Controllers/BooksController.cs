using BookStore.Models.Domain;
using BookStore.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookServices bookServices;
        private readonly IAuthorServices authServices;
        private readonly IGenreServices genreServices;
        private readonly IPublisherServices publisherServices;

        public BooksController(IBookServices bookServices, IAuthorServices authServices, IGenreServices genreServices, IPublisherServices publisherServices)
        {
            this.bookServices = bookServices;
            this.authServices = authServices;
            this.genreServices = genreServices;
            this.publisherServices = publisherServices;
        }

        // GET: Books
        public IActionResult Index()
        {
            var books = bookServices.GetAll();
            return View(books);
        }

        // GET: Books/Details/5
        public IActionResult Details(int id)
        {
            var book = bookServices.FindById(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            var model = new Book
            {
                AuthorList = authServices.GetAll().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString() }).ToList(),
                PublisherList = publisherServices.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString() }).ToList(),
                GenreList = genreServices.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }).ToList()
            };
            return View(model);
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                var result = bookServices.Add(book);
                if (result)
                {
                    TempData["SuccessMessage"] = "Book added successfully.";
                    return RedirectToAction(nameof(Index));
                }
                TempData["ErrorMessage"] = "Unable to add book.";
            }
            // Repopulate lists if the model is not valid
            book.AuthorList = authServices.GetAll().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString() }).ToList();
            book.PublisherList = publisherServices.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString() }).ToList();
            book.GenreList = genreServices.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }).ToList();

            return View(book);
        }

        // GET: Books/Edit/5
        public IActionResult Edit(int id)
        {
            var model = bookServices.FindById(id);
            if (model == null)
            {
                return NotFound();
            }
            model.AuthorList = authServices.GetAll().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString(), Selected = a.Id == model.AuthorId }).ToList();
            model.PublisherList = publisherServices.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString(), Selected = a.Id == model.PublisherId }).ToList();
            model.GenreList = genreServices.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString(), Selected = a.Id == model.GenreId }).ToList();

            return View(model);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Book model)
        {
            if (ModelState.IsValid)
            {
                var result = bookServices.Update(model);
                if (result)
                {
                    TempData["SuccessMessage"] = "Book updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                TempData["ErrorMessage"] = "Unable to update book.";
            }
            // Repopulate lists if the model is not valid
            model.AuthorList = authServices.GetAll().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString(), Selected = a.Id == model.AuthorId }).ToList();
            model.PublisherList = publisherServices.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString(), Selected = a.Id == model.PublisherId }).ToList();
            model.GenreList = genreServices.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString(), Selected = a.Id == model.GenreId }).ToList();

            return View(model);
        }

        // POST: Books/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = bookServices.Delete(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Book deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Unable to delete book.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
