namespace TechMarket.DAL.Entities
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public string ShoppingCartId { get; set; }
        public int ProductId { get; set; }
    }
}
