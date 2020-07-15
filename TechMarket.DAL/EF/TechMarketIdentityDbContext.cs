using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TechMarket.DAL.EF
{
    public class TechMarketIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public TechMarketIdentityDbContext(DbContextOptions<TechMarketIdentityDbContext> options)
: base(options) { }
    }
}
