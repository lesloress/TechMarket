using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TechMarket.BLL.Infrastructure;
using TechMarket.BLL.Interfaces;
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
            options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TechMarketDb;Trusted_Connection=True;MultipleActiveResultSets=true", x => x.MigrationsAssembly("TechMarket.DAL")));
            
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
