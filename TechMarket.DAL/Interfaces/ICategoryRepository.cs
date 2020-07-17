using System.Threading.Tasks;
using TechMarket.DAL.Entities;

namespace TechMarket.DAL.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> GetWithProductsByIdAsync(int id);
        Task<Category> GetCategoryWithoutTracking(int id);
    }
}
