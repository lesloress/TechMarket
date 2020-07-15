using System.Collections.Generic;
using System.Linq;
using TechMarket.BLL.DTO;

namespace TechMarket.Models
{
    public class Cart
    {
        private List<CartItem> lineCollection = new List<CartItem>();

        public virtual void AddItem(ProductDTO product, int quantity)
        {
            CartItem line = lineCollection
            .Where(p => p.Product.Id == product.Id)
            .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartItem
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public virtual void RemoveLine(ProductDTO product) => lineCollection.RemoveAll(l => l.Product.Id == product.Id);
        public virtual decimal GetTotalValue() => lineCollection.Sum(e => e.Product.Price * e.Quantity);
        public virtual void Clear() => lineCollection.Clear();
        public virtual IEnumerable<CartItem> Lines => lineCollection;
    }
    public class CartItem
    {
        public int Id { get; set; }
        public ProductDTO Product { get; set; }
        public int Quantity { get; set; }
    }
}
