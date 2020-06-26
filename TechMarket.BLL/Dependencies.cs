using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TechMarket.BLL.Services;
using TechMarket.DAL.EF;
using TechMarket.DAL.Interfaces;
using TechMarket.DAL.Repositories;

namespace TechMarket.BLL
{
    public class Dependencies
    {
        public static void InjectDependencies(IServiceCollection services)
        {
            services.AddDbContext<TechMarketDbContext>(options => 
            options.UseSqlServer("Server=(local)\\SQLEXPRESS;Database=TechMarketDb;Trusted_Connection=True;", x => x.MigrationsAssembly("TechMarket.DAL")));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            //services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
