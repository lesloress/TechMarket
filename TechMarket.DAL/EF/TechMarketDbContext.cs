using Microsoft.EntityFrameworkCore;
using TechMarket.DAL.Entities;

namespace TechMarket.DAL.EF
{
    public class TechMarketDbContext : DbContext
    {
        public TechMarketDbContext(DbContextOptions<TechMarketDbContext> options)
           : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category[]
                {
                    new Category { Id=1, Name="Laptops"},
                    new Category { Id=2, Name="Desktops&All-in-ones"},
                    new Category { Id=3, Name="Smartphones"}
                });

            modelBuilder.Entity<Product>().HasData(
                new Product[]
                {
                    new Product {
                        Id = 1, 
                        Name="Laptop1", 
                        Description="Cool laptop", 
                        Price=900, 
                        ImagePath="default_product.jpg", 
                        CategoryId=1},
                    new Product {
                        Id = 2,
                        Name="lapTop2",
                        Description="Another cool laptop",
                        Price=850,
                        ImagePath="asusadol203639700.jpg",
                        CategoryId=1},
                    new Product {
                        Id = 3,
                        Name="Smartphone1",
                        Description="Cool smartphone",
                        Price=650,
                        ImagePath="default_product.jpg",
                        CategoryId=3},
                });
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
