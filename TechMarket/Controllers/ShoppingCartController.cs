using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechMarket.BLL.DTO;
using TechMarket.BLL.Interfaces;
using TechMarket.Models;

namespace TechMarket.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IProductService _productService;
        public ShoppingCartController(IShoppingCartService shoppingCartService, 
            IProductService productService)
        {
            _shoppingCartService = shoppingCartService;
            _productService = productService;
        }

        public async Task<IActionResult> ShoppingCartList()
        {
            var model = new ShoppingCartVM()
            {
                ShoppingCartItems = await _shoppingCartService.GetAllShoppingCartItems(GetCartId())
            };
            return View(model);
        }

        [HttpPost]
        public async Task AddToCart(int productId, int quantity = 1)
        {
            ProductDTO product = await _productService.GetProductById(productId);
            if (product != null)
            {
                ShoppingCartItemDTO cartItem = new ShoppingCartItemDTO()
                {
                    Quantity = quantity,
                    ShoppingCartId = GetCartId(),
                    ProductId = productId
                };
                await _shoppingCartService.AddToCart(cartItem);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFromCart(int cartItemId)
        {
            await _shoppingCartService.RemoveById(cartItemId);
            return RedirectToAction("ShoppingCartList");
        }

        private string GetCartId()
        {
            string cartId = HttpContext.Session.GetString("CartId") ?? Guid.NewGuid().ToString();
            HttpContext.Session.SetString("CartId", cartId);
            return cartId;
        }
    }
}