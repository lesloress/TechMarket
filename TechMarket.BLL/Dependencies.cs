using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
        public static void InjectDependencies(IServiceCollection services, string connection, string identityConnection)
        {
            services.AddDbContext<TechMarketDbContext>(options =>
            options.UseSqlServer(
                connection,
                x => x.MigrationsAssembly("TechMarket.DAL")));

            services.AddDbContext<TechMarketIdentityDbContext>(options =>
            options.UseSqlServer(
                identityConnection,
                x => x.MigrationsAssembly("TechMarket.DAL")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<TechMarketIdentityDbContext>()
                .AddDefaultTokenProviders();

            InjectRepositories(services);
            InjectServices(services);
            CreateMapper(services);
        }

        private static void InjectRepositories(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void InjectServices(IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<IOrderService, OrderService>();
        }

        private static void CreateMapper(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
