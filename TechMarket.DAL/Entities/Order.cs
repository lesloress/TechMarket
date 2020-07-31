using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechMarket.DAL.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public IEnumerable<ShoppingCartItem> CartItems { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        public bool Shipped { get; set; }
    }
}
