using System.Collections.Generic;
using System.Threading.Tasks;
using TechMarket.BLL.DTO;

namespace TechMarket.BLL.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDTO> GetCategoryById(int id);
        Task<IEnumerable<CategoryDTO>> GetAllCategories();
        Task<CategoryDTO> CreateCategory(CategoryDTO categoryDto);
        Task DeleteCategory(CategoryDTO categoryDTO);
        Task<bool> DeleteCategoryById(int id);
        Task UpdateCategory(CategoryDTO categoryDto);

    }
}
