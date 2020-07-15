using System.Collections.Generic;
using System.Threading.Tasks;
using TechMarket.DAL.Entities;

namespace TechMarket.DAL.Interfaces
{
    public interface IShoppingCartRepository : IRepository<ShoppingCartItem>
    {
        Task<IEnumerable<ShoppingCartItem>> GetByCartIdWithProductAsync(string shoppingCartId);
        Task<ShoppingCartItem> GetByCartAndProductIds(string cartId, int productId);
    }
}
