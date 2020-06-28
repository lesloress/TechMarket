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

        public HomeController(/*ILogger<HomeController> logger*/ICategoryService categoryService)
        {
            _categoryService = categoryService;
            //_logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            CatalogPageVM model = new CatalogPageVM(await _categoryService.GetAllCategories());
            //model.AvailableCategories = _mapper.Map<IList<SelectListItem>>(await _categoryService.GetAllCategories());
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
