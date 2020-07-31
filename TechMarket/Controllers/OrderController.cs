using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechMarket.BLL.DTO;
using TechMarket.BLL.Interfaces;
using TechMarket.Infrastructure;

namespace TechMarket.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IProductService _productService;

        public OrderController(IOrderService orderService,
            IShoppingCartService shoppingCartService,
            IProductService productService)
        {
            _orderService = orderService;
            _shoppingCartService = shoppingCartService;
            _productService = productService;
        }

        [HttpGet]
        public ViewResult Checkout() => View(new OrderDTO());

        [HttpPost]
        public async Task<IActionResult> Checkout(OrderDTO order)
        {
            var cartId = HttpContext.Session.GetCartId();
            if(!await _shoppingCartService.IsNotEmpty(cartId))
            {
                ModelState.AddModelError("", "Your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                await _orderService.SaveOrder(order, cartId);
                return RedirectToAction("OrderCompleted");
            }
            else
            {
                return View(order);
            }
        }

        public async Task<IActionResult> OrderCompleted()
        {
            await _shoppingCartService
                .ClearCart(HttpContext.Session.GetCartId());
            return View();
        }

        [Authorize]
        public async Task<ViewResult> OrdersManager() => 
            View(await _orderService.GetNotShippedOrders());

        [HttpPost]
        public async Task<IActionResult> MarkShipped(int id)
        {
            OrderDTO order = await _orderService.GetOrderById(id);
            if (order != null)
            {
                await _orderService.MarkShipped(order);
            }
            return RedirectToAction("OrdersManager");
        }
    } 
}