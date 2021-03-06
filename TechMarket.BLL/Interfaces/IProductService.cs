﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TechMarket.BLL.DTO;

namespace TechMarket.BLL.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProducts();
        Task<ProductDTO> GetProductById(int id);
        Task<IEnumerable<ProductDTO>> GetProductsByCategoryId(int categoryId);
        Task<IEnumerable<ProductDTO>> FilterProducts(IList<int> selectedCategoriesIds);
        Task<IEnumerable<ProductDTO>> FindProductsByNameAndDescription(string text);
        Task<ProductDTO> CreateProduct(ProductDTO productDto);
        Task UpdateProduct(ProductDTO productDto);
        Task DeleteProduct(ProductDTO product);
        Task<bool> DeleteProductById(int id);
    }
}
