using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechMarket.BLL.DTO;

namespace TechMarket.Models
{
    public class OrdersManagerVM
    {
        public OrderDTO Order { get; set; }
        public IEnumerable<ShoppingCartItemDTO> ShoppingCartItems { get; set; }
    }
}
