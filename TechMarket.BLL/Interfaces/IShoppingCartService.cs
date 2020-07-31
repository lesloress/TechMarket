using System.Collections.Generic;
using System.Threading.Tasks;
using TechMarket.BLL.DTO;

namespace TechMarket.BLL.Interfaces
{
    public interface IShoppingCartService
    {
        Task AddToCart(ShoppingCartItemDTO item);
        Task<IEnumerable<ShoppingCartItemDTO>> GetAllShoppingCartItems(string shoppingCartId);
        Task RemoveById(int cartItemId);
        Task ClearCart(string cartId);
        Task<bool> IsNotEmpty(string cartId);
    }
}
