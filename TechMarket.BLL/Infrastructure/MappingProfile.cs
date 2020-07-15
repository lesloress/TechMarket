using AutoMapper;
using TechMarket.BLL.DTO;
using TechMarket.DAL.Entities;

namespace TechMarket.BLL.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();
            CreateMap<ShoppingCartItem, ShoppingCartItemDTO>()
                .ForMember("Name", opt => opt.MapFrom(p => p.Product.Name))
                .ForMember("Price", opt => opt.MapFrom(p => p.Product.Price))
                .ForMember("ImagePath", opt => opt.MapFrom(p => p.Product.ImagePath));
            CreateMap<ShoppingCartItemDTO, ShoppingCartItem>();
        }
    }
}
