using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using TechMarket.BLL.Interfaces;
using TechMarket.Models;

namespace TechMarket.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public HomeController(/*ILogger<HomeController> logger*/ICategoryService categoryService,
            IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
            //_logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            CatalogPageVM model = new CatalogPageVM(await _categoryService.GetAllCategories(), 
                await _productService.GetAllProducts());
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string searchText)
        {
            CatalogPageVM model;
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                model = new CatalogPageVM(await _categoryService.GetAllCategories(),
                    await _productService.FindProductsByNameAndDescription(searchText));
            } else
            {
                model = new CatalogPageVM(await _categoryService.GetAllCategories(),
                    await _productService.GetAllProducts());
            }
            return View(model);
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
