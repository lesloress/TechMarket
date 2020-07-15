
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using TechMarket.BLL.DTO;

namespace TechMarket.Models
{
    public class ProductEditorVM
    {
        public ProductDTO Product { get; set; }
        public SelectList Categories { get; set; }
        [Display(Name = "Upload image")]
        public IFormFile ImageFile { get; set; }
    }
}
