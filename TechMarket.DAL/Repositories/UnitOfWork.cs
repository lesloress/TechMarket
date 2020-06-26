using System.Threading.Tasks;
using TechMarket.DAL.EF;
using TechMarket.DAL.Interfaces;

namespace TechMarket.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TechMarketDbContext _context;
        private ProductRepository _productRepository;
        private CategoryRepository _categoryRepository;

        public UnitOfWork(TechMarketDbContext context)
        {
            this._context = context;
        }
        public IProductRepository Products => _productRepository = _productRepository ?? new ProductRepository(_context);
        public ICategoryRepository Categories => _categoryRepository = _categoryRepository ?? new CategoryRepository(_context);

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
