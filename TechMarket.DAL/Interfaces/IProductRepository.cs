using System.Collections.Generic;
using System.Threading.Tasks;
using TechMarket.DAL.Entities;

namespace TechMarket.DAL.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetWithCategoryByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllWithCategoryAsync();
        Task<IEnumerable<Product>> GetAllWithCategoryByCategoryIdAsync(int categoryId);
        Task<Product> GetProductWithoutTracking(int id);
    }
}
