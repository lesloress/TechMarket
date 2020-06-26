using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechMarket.DAL.Entities
{
    public class Category
    {
        public Category()
        {
            Products = new List<Product>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public IList<Product> Products { get; set; }
    }
}
