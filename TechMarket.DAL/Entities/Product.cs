using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechMarket.DAL.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string ImagePath { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
