using BookStore.Models.Domain;
using BookStore.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class GenresController : Controller
    {
        private readonly IGenreServices _genreServices;

        public GenresController(IGenreServices genreServices)
        {
            _genreServices = genreServices;
        }

        public IActionResult Index()
        {
            var genres = _genreServices.GetAll();
            return View(genres);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Genre model, string action)
        {
            if (action == "Save")
            {
                if (ModelState.IsValid)
                {
                    var result = _genreServices.Add(model);
                    if (result)
                    {
                        TempData["SuccessMessage"] = "Genre added successfully.";
                        return RedirectToAction("Add");
                    }

                    TempData["ErrorMessage"] = "Unable to add genre.";
                }
            }
            else if (action == "List")
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var genre = _genreServices.FindById(id);
            if (genre == null)
                return NotFound();

            return View(genre);
        }

        [HttpPost]
        public IActionResult Edit(Genre model)
        {
            if (ModelState.IsValid)
            {
                var result = _genreServices.Update(model);
                if (result)
                {
                    TempData["SuccessMessage"] = "Genre updated successfully.";
                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Unable to update genre.";
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var result = _genreServices.Delete(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Genre deleted successfully.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Unable to delete genre.";
            return RedirectToAction("Index");
        }
    }
}
