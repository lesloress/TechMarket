using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechMarket.DAL.EF;
using TechMarket.DAL.Entities;
using TechMarket.DAL.Interfaces;

namespace TechMarket.DAL.Repositories
{
    public class ShoppingCartRepository : Repository<ShoppingCartItem>, IShoppingCartRepository
    {
        public ShoppingCartRepository(TechMarketDbContext context) : base(context) { }
        public async Task<IEnumerable<ShoppingCartItem>> GetByCartIdWithProductAsync(string shoppingCartId)
        {
            return await TechMarketDbContext.ShoppingCartItems
               .Include(i => i.Product)
               .Where(i => i.ShoppingCartId == shoppingCartId)
               .ToListAsync();
        }

        public async Task<ShoppingCartItem> GetByCartAndProductIds(string cartId, int productId)
        {
            return await TechMarketDbContext.ShoppingCartItems.Where(
                s => s.ShoppingCartId == cartId && s.ProductId == productId)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsNotEmpty(string cartId)
        {
            return await TechMarketDbContext
                .ShoppingCartItems.AnyAsync(s => s.ShoppingCartId == cartId);
        }

        private TechMarketDbContext TechMarketDbContext
        {
            get { return context as TechMarketDbContext; }
        }
    }
}
