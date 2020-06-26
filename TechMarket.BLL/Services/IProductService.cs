using System.Collections.Generic;
using System.Threading.Tasks;
using TechMarket.DAL.Entities;

namespace TechMarket.BLL.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<Product> CreateProduct(Product product);
        Task UpdateProduct(Product productToBeUpdated, Product product);
        Task DeleteProduct(Product product);
    }
}
