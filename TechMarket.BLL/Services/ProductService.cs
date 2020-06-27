using System.Collections.Generic;
using System.Threading.Tasks;
using TechMarket.BLL.DTO;
using TechMarket.DAL.Interfaces;
using TechMarket.BLL.Infrastructure.Mappers;
using TechMarket.DAL.Entities;
using TechMarket.BLL.Interfaces;

namespace TechMarket.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductDTO> CreateProduct(ProductDTO productDto)
        {
            Product product = ProductMapper.ToProduct(productDto);
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.CommitAsync();
            return productDto;
        }

        public async Task<ProductDTO> GetProductById(int id)
        { 
            return ProductMapper
                .ToProductDTO(await _unitOfWork.Products.GetByIdAsync(id));
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            return ProductMapper.ToProductDTO(await _unitOfWork.Products.GetAllAsync());
        }

        public async Task DeleteProduct(ProductDTO productDto)
        {
            Product product = ProductMapper.ToProduct(productDto);
            _unitOfWork.Products.Remove(product);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateProduct(ProductDTO productDto)
        {
            Product product = ProductMapper.ToProduct(productDto);
            _unitOfWork.Products.Update(product);
            await _unitOfWork.CommitAsync();
        }
    }
}
