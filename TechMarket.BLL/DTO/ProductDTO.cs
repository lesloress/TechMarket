
using System.ComponentModel.DataAnnotations;

namespace TechMarket.BLL.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid price")]
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public int CategoryId { get; set; }
    }
}
