using System.Collections.Generic;
using System.Linq;
using TechMarket.BLL.DTO;

namespace TechMarket.Models
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCartItemDTO> ShoppingCartItems { get; set; }
        public decimal TotalPrice => ShoppingCartItems.Sum(p => p.FullPrice);
    }
}
