using System.Collections.Generic;
using System.Threading.Tasks;
using TechMarket.BLL.DTO;
using TechMarket.DAL.Interfaces;
using TechMarket.DAL.Entities;
using TechMarket.BLL.Interfaces;
using AutoMapper;

namespace TechMarket.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductDTO> CreateProduct(ProductDTO productDto)
        {
            Product product = _mapper.Map<Product>(productDto);
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.CommitAsync();
            return productDto;
        }

        public async Task<ProductDTO> GetProductById(int id)
        {
            return _mapper.Map<ProductDTO>(await _unitOfWork.Products.GetByIdAsync(id));
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsByCategoryId(int categoryId)
        {
            return _mapper.Map<IEnumerable<ProductDTO>>(await _unitOfWork.Products.Find(p => p.CategoryId == categoryId));
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            return _mapper.Map<IEnumerable<ProductDTO>>(await _unitOfWork.Products.GetAllAsync());
        }

        public async Task DeleteProduct(ProductDTO productDto)
        {
            Product product = _mapper.Map<Product>(productDto);
            _unitOfWork.Products.Remove(product);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateProduct(ProductDTO productDto)
        {
            Product product = _mapper.Map<Product>(productDto);
            _unitOfWork.Products.Update(product);
            await _unitOfWork.CommitAsync();
        }
    }
}
