using TechMarket.DAL.EF;
using TechMarket.DAL.Entities;
using TechMarket.DAL.Interfaces;

namespace TechMarket.DAL.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(TechMarketDbContext context) : base(context) { }
        private TechMarketDbContext TechMarketDbContext
        {
            get { return context as TechMarketDbContext; }
        }
    }
}
