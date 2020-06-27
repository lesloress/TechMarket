using AutoMapper;
using System.Collections.Generic;
using TechMarket.BLL.DTO;
using TechMarket.DAL.Entities;

namespace TechMarket.BLL.Infrastructure.Mappers
{
    public static class ProductMapper
    {
        public static IMapper Mapper;
        static ProductMapper()
        {
            Mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDTO>();
                cfg.CreateMap<ProductDTO, Product>();
            }).CreateMapper();
        }
        public static ProductDTO ToProductDTO(this Product product)
        {
            return Mapper.Map<Product, ProductDTO>(product);
        }

        public static IEnumerable<ProductDTO> ToProductDTO(this IEnumerable<Product> products)
        {
            return Mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
        }

        public static Product ToProduct(this ProductDTO productDto)
        {
            return Mapper.Map<ProductDTO, Product>(productDto);
        }
        public static IEnumerable<Product> ToProduct(this IEnumerable<ProductDTO> productDtos)
        {
            return Mapper.Map<IEnumerable<ProductDTO>, IEnumerable<Product>>(productDtos);
        }

    }
}
