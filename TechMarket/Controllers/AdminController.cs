using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechMarket.BLL.DTO;
using TechMarket.BLL.Interfaces;
using TechMarket.Models;

namespace TechMarket.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private const string ProductImagesFolder = "/product_images/";

        public AdminController(IProductService productService, 
            ICategoryService categoryService, 
            IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
        }

        public ActionResult AdminPanel() => View();

        public async Task<IActionResult> ProductList() => 
            View("ProductsManager", await _productService.GetAllProducts());

        public async Task<IActionResult> CategoryList() =>
            View("CategoriesManager", await _categoryService.GetAllCategories());

        //public async Task<IActionResult> ProductListPartial() => 
        //    PartialView("_AdminProductList", await _productService.GetAllProducts());

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
                //ViewBag.ModelName = "category";
                //return PartialView("Success");
                TempData["message"] = "New category was successfully created";
                return RedirectToAction("CategoryList");
            }
            return PartialView("_CreateCategory", category);
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (await _categoryService.DeleteCategoryById(id))
                TempData["message"] = "Category was successfully deleted";
            else
                TempData["message"] = "Error";
            return RedirectToAction("CategoryList");
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
                return NotFound();
            var categories = await _categoryService.GetAllCategories();
            return View(new ProductEditorVM { Product = product, Categories = new SelectList(categories, "Id", "Name") });
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductEditorVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageFile != null)
                {
                    string fileName = model.Product.ImagePath = CreateFileName(model.ImageFile);
                    await SaveImageToFolder(ProductImagesFolder, fileName, model.ImageFile);
                }
                await _productService.UpdateProduct(model.Product);
                return RedirectToAction("ProductList");
            }
                
            model.Categories = new SelectList(await _categoryService.GetAllCategories(), "Id", "Name");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            //var product = await _productService.GetProductById(id);
            if (await _productService.DeleteProductById(id))
                TempData["message"] = "Product was successfully deleted";
            else
                TempData["message"] = "Error";
            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var categories = await _categoryService.GetAllCategories();
            var model = new ProductEditorVM { Categories = new SelectList(categories, "Id", "Name") };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductEditorVM model)
        {
            if (ModelState.IsValid)
            {
                string fileName = model.Product.ImagePath = CreateFileName(model.ImageFile);
                await SaveImageToFolder(ProductImagesFolder, fileName, model.ImageFile);

                await _productService.CreateProduct(model.Product);
                return RedirectToAction("ProductList");

            }
                
            model.Categories = new SelectList(await _categoryService.GetAllCategories(), "Id", "Name");
            return View(model);
        }

        private string CreateFileName(IFormFile file)
        {
            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
            string fileExt = Path.GetExtension(file.FileName);
            return fileName + DateTime.Now.ToString("yymmssfff") + fileExt;
        }

        private async Task SaveImageToFolder(string folderName, string fileName, IFormFile file)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string path = Path.Combine(wwwRootPath + folderName, fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }
    }
}