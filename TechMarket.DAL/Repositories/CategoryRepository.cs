using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TechMarket.DAL.EF;
using TechMarket.DAL.Entities;
using TechMarket.DAL.Interfaces;

namespace TechMarket.DAL.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(TechMarketDbContext context) : base(context) { }

        public async Task<Category> GetWithProductsByIdAsync(int id)
        {
            return await techMarketDbContext.Categories
                           .Include(c => c.Products)
                           .SingleOrDefaultAsync(m => m.Id == id);
        }
        private TechMarketDbContext techMarketDbContext
        {
            get { return context as TechMarketDbContext; }
        }
    }
}
