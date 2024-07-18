using BookStore.Models.Domain;
using BookStore.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class PublishersController : Controller
    {
        private readonly IPublisherServices _publisherServices;

        public PublishersController(IPublisherServices publisherServices)
        {
            _publisherServices = publisherServices;
        }

        public IActionResult Index()
        {
            var publishers = _publisherServices.GetAll();
            return View(publishers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                var result = _publisherServices.Add(publisher);
                if (result)
                {
                    TempData["SuccessMessage"] = "Publisher added successfully.";
                    return RedirectToAction(nameof(Index));
                }
                TempData["ErrorMessage"] = "Unable to add publisher.";
            }
            return View(publisher);
        }

        public IActionResult Edit(int id)
        {
            var publisher = _publisherServices.FindById(id);
            if (publisher == null)
                return NotFound();

            return View(publisher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                var result = _publisherServices.Update(publisher);
                if (result)
                {
                    TempData["SuccessMessage"] = "Publisher updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                TempData["ErrorMessage"] = "Unable to update publisher.";
            }
            return View(publisher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = _publisherServices.Delete(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Publisher deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Unable to delete publisher.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
