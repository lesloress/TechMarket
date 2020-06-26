using Microsoft.EntityFrameworkCore;
using TechMarket.DAL.Entities;

namespace TechMarket.DAL.EF
{
    public class TechMarketDbContext : DbContext
    {
        public TechMarketDbContext(DbContextOptions<TechMarketDbContext> options)
           : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
