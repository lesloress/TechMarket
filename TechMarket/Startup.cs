using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TechMarket.BLL;
using TechMarket.Infrastructure;

namespace TechMarket
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Dependencies.InjectDependencies(services, 
                Configuration.GetConnectionString("TechMarketDb"),
                Configuration.GetConnectionString("TechMarketIdentity"));
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});
            //services.AddSession(opts =>
            //{
            //    opts.Cookie.IsEssential = true; // make the session cookie Essential
            //});
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //services.AddScoped(sp => ShoppingCartExtensions.GetCart(sp));
            //services.AddScoped(sp => SessionMethods.GetCartId(sp));

            services.AddMemoryCache();

            services.AddSession();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            

            app.UseSession();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            IdentityDataInitializer.EnsurePopulated(app);
        }
    }
}
