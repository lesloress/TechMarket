using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechMarket.BLL.DTO;
using TechMarket.BLL.Interfaces;
using TechMarket.Infrastructure;
using TechMarket.Models;

namespace TechMarket.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private const string ProductImagesFolder = "/product_images/";

        public ProductController(IProductService productService, 
            ICategoryService categoryService, 
            IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize]
        public async Task<IActionResult> ProductList() =>
            View("ProductsManager", await _productService.GetAllProducts());


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
                return NotFound();
            var categories = await _categoryService.GetAllCategories();
            return View(new ProductEditorVM { Product = product, Categories = new SelectList(categories, "Id", "Name") });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditProduct(ProductEditorVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageFile != null)
                {
                    string fileName = model.Product.ImagePath = ImagesManager.CreateFileName(model.ImageFile);
                    await ImagesManager.SaveImageToFolder(_webHostEnvironment.WebRootPath,
                        ProductImagesFolder, fileName, model.ImageFile);
                }
                await _productService.UpdateProduct(model.Product);
                return RedirectToAction("ProductList");
            }

            model.Categories = new SelectList(await _categoryService.GetAllCategories(), "Id", "Name");
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (await _categoryService.DeleteCategoryById(id))
                TempData["message"] = "Category was successfully deleted";
            else
                TempData["message"] = "Error";
            return RedirectToAction("CategoryList");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (await _productService.DeleteProductById(id))
                TempData["message"] = "Product was successfully deleted";
            else
                TempData["message"] = "Error";
            return RedirectToAction("ProductList");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CreateProduct()
        {
            var categories = await _categoryService.GetAllCategories();
            var model = new ProductEditorVM { Categories = new SelectList(categories, "Id", "Name") };
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProduct(ProductEditorVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageFile != null)
                {
                    string fileName = model.Product.ImagePath = ImagesManager.CreateFileName(model.ImageFile);
                    await ImagesManager.SaveImageToFolder(_webHostEnvironment.WebRootPath, 
                        ProductImagesFolder, fileName, model.ImageFile);
                }
                else
                {
                    model.Product.ImagePath = "default_product.jpg";
                }

                await _productService.CreateProduct(model.Product);
                return RedirectToAction("ProductList");

            }

            model.Categories = new SelectList(await _categoryService.GetAllCategories(), "Id", "Name");
            return View(model);
        }


        public async Task<IActionResult> ProductListPartial()
        {
            return PartialView("_ProductList", await _productService.GetAllProducts());
        }

        [HttpPost]
        public async Task<IActionResult> ProductListPartial(IList<string> SelectedCategoriesIds, SortState CurrentSortState)
        {
            IEnumerable<ProductDTO> model;
            if (SelectedCategoriesIds.Count > 0)
            {
                model = await _productService
                .FilterProducts(SelectedCategoriesIds.Select(int.Parse).ToList());
            } else
            {
                model = await _productService.GetAllProducts();
            }

            if (CurrentSortState == SortState.PriceAsc)
            {
                model = model.OrderBy(m => m.Price);
            } else if (CurrentSortState == SortState.PriceDesc)
            {
                model = model.OrderByDescending(m => m.Price);
            }
            return PartialView("_ProductList", model);
        }

        public async Task<IActionResult> ProductDetails(int id)
        {
            return View(await _productService.GetProductById(id));
        }

    }
}