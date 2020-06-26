using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechMarket.DAL.EF;
using TechMarket.DAL.Entities;
using TechMarket.DAL.Interfaces;

namespace TechMarket.DAL.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(TechMarketDbContext context) : base(context) { }

        public async Task<Product> GetWithCategoryByIdAsync(int id)
        {
            return await TechMarketDbContext.Products
                .Include(p => p.Category)
                .SingleOrDefaultAsync(m => m.Id == id); 
        }

        public async Task<IEnumerable<Product>> GetAllWithCategoryAsync()
        {
            return await TechMarketDbContext.Products
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllWithCategoryByCategoryIdAsync(int categoryId)
        {
            return await TechMarketDbContext.Products
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

        private TechMarketDbContext TechMarketDbContext
        {
            get { return context as TechMarketDbContext; }
        }
    }
}
