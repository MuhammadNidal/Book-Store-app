using BookStore.Models.Domain;
using BookStore.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorServices _authorServices;

        public AuthorsController(IAuthorServices authorServices)
        {
            _authorServices = authorServices;
        }

        public IActionResult Index()
        {
            var authors = _authorServices.GetAll();
            return View(authors);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Author author)
        {
            if (ModelState.IsValid)
            {
                var result = _authorServices.Add(author);
                if (result)
                {
                    TempData["SuccessMessage"] = "Author added successfully.";
                    return RedirectToAction(nameof(Index));
                }
                TempData["ErrorMessage"] = "Unable to add author.";
            }
            return View(author);
        }

        public IActionResult Edit(int id)
        {
            var author = _authorServices.FindById(id);
            if (author == null)
                return NotFound();

            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Author author)
        {
            if (ModelState.IsValid)
            {
                var result = _authorServices.Update(author);
                if (result)
                {
                    TempData["SuccessMessage"] = "Author updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                TempData["ErrorMessage"] = "Unable to update author.";
            }
            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = _authorServices.Delete(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Author deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Unable to delete author.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
