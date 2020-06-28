using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using TechMarket.BLL.Interfaces;

namespace TechMarket.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IProductService productService, IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
           _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> ProductListPartial()
        {
            return PartialView("ProductList_", await _productService.GetAllProducts());
        }

        public async Task<IActionResult> ProductDetails(int id)
        {
            return View(await _productService.GetProductById(id));
        }

        //public async Task<IActionResult> GetAllCategories()
        //{
        //    //return await 
        //}
    }
}