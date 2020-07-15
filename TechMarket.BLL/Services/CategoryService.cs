using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechMarket.BLL.DTO;
using TechMarket.BLL.Interfaces;
using TechMarket.DAL.Entities;
using TechMarket.DAL.Interfaces;

namespace TechMarket.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> GetCategoryById(int id)
        {
            return _mapper.Map<CategoryDTO>(await _unitOfWork.Categories.GetByIdAsync(id));
        }
        public async Task<IEnumerable<CategoryDTO>> GetAllCategories()
        {
            return _mapper.Map<IEnumerable<CategoryDTO>>(await _unitOfWork.Categories.GetAllAsync());
        }
        public async Task<CategoryDTO> CreateCategory(CategoryDTO categoryDto)
        {
            Category category = _mapper.Map<Category>(categoryDto);
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.CommitAsync();
            return categoryDto;
        }
    }
}
