using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechMarket.BLL.DTO;
using TechMarket.BLL.Interfaces;

namespace TechMarket.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> CategoryList() =>
            View("CategoriesManager", await _categoryService.GetAllCategories());

        [HttpGet]
        public ActionResult CreateCategory()
        {
            return PartialView("_CreateCategory");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(CategoryDTO category)
        {
            if (ModelState.IsValid)
            {
                category.Id = 0;
                await _categoryService.CreateCategory(category);
                ViewBag.Message = "New category was successfully added!";
                return PartialView("Success");
            }
            return PartialView("_CreateCategory", category);
        }

        [HttpGet]
        public async Task<IActionResult> EditCategory(int id)
        {
            var model = await _categoryService.GetCategoryById(id);
            return PartialView("_EditCategory", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(CategoryDTO category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.UpdateCategory(category);
                ViewBag.Message = "Your category was successfully edited!";
                return PartialView("Success");
            }
            return PartialView("_EditCategory", category);
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (await _categoryService.DeleteCategoryById(id))
                TempData["message"] = "Category was successfully deleted";
            else
                TempData["message"] = "Error";
            return RedirectToAction("CategoryList");
        }
    }
}