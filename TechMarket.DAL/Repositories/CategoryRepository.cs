﻿using Microsoft.EntityFrameworkCore;
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
            return await TechMarketDbContext.Categories
                           .Include(c => c.Products)
                           .SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Category> GetCategoryWithoutTracking(int id)
        {
            return await TechMarketDbContext.Categories.SingleOrDefaultAsync(p => p.Id == id);
        }

        private TechMarketDbContext TechMarketDbContext
        {
            get { return context as TechMarketDbContext; }
        }
    }
}
