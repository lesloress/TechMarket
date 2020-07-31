using System.Threading.Tasks;
using TechMarket.DAL.EF;
using TechMarket.DAL.Interfaces;

namespace TechMarket.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TechMarketDbContext _context;
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        private IShoppingCartRepository _shoppingCartRepository;
        private IOrderRepository _orderRepository;

        public UnitOfWork(TechMarketDbContext context)
        {
            _context = context;
        }
        public IProductRepository Products => _productRepository = _productRepository ?? new ProductRepository(_context);
        public ICategoryRepository Categories => _categoryRepository = _categoryRepository ?? new CategoryRepository(_context);
        public IShoppingCartRepository ShoppingCartRepository => 
            _shoppingCartRepository = _shoppingCartRepository ?? new ShoppingCartRepository(_context);
        public IOrderRepository Orders => _orderRepository ?? new OrderRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
