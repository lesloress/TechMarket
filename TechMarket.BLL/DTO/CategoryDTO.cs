
using System.ComponentModel.DataAnnotations;

namespace TechMarket.BLL.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
