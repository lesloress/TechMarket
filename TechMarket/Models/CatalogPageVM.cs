using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TechMarket.BLL.DTO;

namespace TechMarket.Models
{
    public class CatalogPageVM
    {
        public IEnumerable<ProductDTO> Products { get; set; }
        public SortState CurrentSortState { get; set; } = SortState.None;
        public SelectList AvailableSortStates { get; set; }
        public IList<string> SelectedCategoriesIds { get; set; }
        public IList<SelectListItem> AvailableCategories { get; set; }

        public CatalogPageVM(IEnumerable<CategoryDTO> categories, IEnumerable<ProductDTO> products)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, SelectListItem>()
                .ForMember("Text", opt => opt.MapFrom(c => c.Name))
                .ForMember("Value", opt => opt.MapFrom(c => c.Id)));

            var mapper = new Mapper(config);

            AvailableCategories = mapper.Map<IEnumerable<CategoryDTO>, IList<SelectListItem>>(categories);

            SelectedCategoriesIds = new List<string>();

            Products = products;
        }
    }

    public enum SortState
    {
        None,
        [Display(Name = "Price ascending")]
        PriceAsc,
        [Display(Name = "Price descending")]
        PriceDesc
    }
}
