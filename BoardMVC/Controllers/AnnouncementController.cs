using BoardMVC.DTO;
using BoardMVC.Models;
using BoardMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BoardMVC.Controllers
{
    public class AnnouncementController : Controller
    {
        private readonly AnnouncementService _service;

        public AnnouncementController(AnnouncementService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {            
            var announcements = await _service.GetAllAsync();

            var categories = (await _service.GetAllCategories()).ToDictionary(c => c.Id, c => c.CategoryDisplayName);
            var subCategories = (await _service.GetAllSubCategories()).ToDictionary(c => c.SubCategoryId, c => c.SubCategoryDisplayName);

            foreach (var announcement in announcements)
            {
                if (categories.TryGetValue(announcement.CategoryId.Value, out var name))
                {
                    announcement.CategoryName = name;
                }

                if (subCategories.TryGetValue(announcement.SubCategoryId.Value, out var subName))
                {
                    announcement.SubCategoryName = subName;
                }
            }

            return View(announcements);
        }

        public async Task<IActionResult> Details(int id)
        {
            var announcement = await _service.GetByIdAsync(id);

            if (announcement == null)
            {
                return NotFound();
            }

            announcement.CategoryName = (await _service.GetCategorнByIdAsync(announcement.CategoryId.Value)).CategoryName;
            announcement.SubCategoryName = (await _service.GetSubCategoryByIdAsync(announcement.SubCategoryId.Value)).SubCategoryName;

            return View(announcement);
        }

        public IActionResult Create() {

            var categories = _service.GetAllCategories().ConfigureAwait(false).GetAwaiter().GetResult();
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryDisplayName");
            var subCategories = _service.GetAllSubCategories().ConfigureAwait(false).GetAwaiter().GetResult();
            ViewBag.SubCategories = new SelectList(subCategories, "SubCategoryId", "SubCategoryDisplayName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AnnouncementDto announcement)
        {
            if (!ModelState.IsValid)
            {
                var categories = _service.GetAllCategories().ConfigureAwait(false).GetAwaiter().GetResult();
                ViewBag.Categories = new SelectList(categories, "Id", "CategoryDisplayName");

                var subCategories = _service.GetAllSubCategories().ConfigureAwait(false).GetAwaiter().GetResult();
                ViewBag.SubCategories = new SelectList(subCategories, "SubCategoryId", "SubCategoryDisplayName");

                return View(announcement);
            }

            await _service.CreateAsync(announcement);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var announcement = await _service.GetByIdAsync(id);
            if (announcement == null)
            {
                return NotFound();
            }

            var categories = await _service.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryDisplayName", announcement.CategoryId);

            var subCategories = _service.GetAllSubCategories().ConfigureAwait(false).GetAwaiter().GetResult();
            ViewBag.SubCategories = new SelectList(subCategories, "SubCategoryId", "SubCategoryDisplayName", announcement.SubCategoryId);

            return View(announcement);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(announcement);
                return RedirectToAction(nameof(Index));
            }

            var categories = await _service.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryDisplayName", announcement.CategoryId);

            var subCategories = _service.GetAllSubCategories().ConfigureAwait(false).GetAwaiter().GetResult();
            ViewBag.SubCategories = new SelectList(subCategories, "SubCategoryId", "SubCategoryDisplayName", announcement.SubCategoryId);

            return View(announcement);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var announcement = await _service.GetByIdAsync(id);
            return announcement == null ? NotFound() : View(announcement);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}