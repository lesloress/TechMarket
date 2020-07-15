using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using TechMarket.BLL.DTO;
using TechMarket.BLL.Interfaces;
using TechMarket.Models;

namespace TechMarket.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
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