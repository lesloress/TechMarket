using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace TechMarket.Infrastructure
{
    public static class IdentityDataInitializer
    {
        private const string adminUser = "Admin";
        private const string adminPassword = "Startup1$";
        public static async void EnsurePopulated(IApplicationBuilder app)
        {

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                        .CreateScope())
            {
                UserManager<IdentityUser> userManager = serviceScope.ServiceProvider.GetService<UserManager<IdentityUser>>();
                IdentityUser user = await userManager.FindByIdAsync(adminUser);
                if (user == null)
                {
                    user = new IdentityUser("Admin");
                    await userManager.CreateAsync(user, adminPassword);
                }
            }
        }
    }
}
