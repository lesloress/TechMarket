using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TechMarket.DAL.EF
{
    public class TechMarketIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public TechMarketIdentityDbContext(
            DbContextOptions<TechMarketIdentityDbContext> options): base(options) {
            Database.EnsureCreated();
        }
    }
}
