namespace TechMarket.BLL.DTO
{
    public class ShoppingCartItemDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string ShoppingCartId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }

        public decimal FullPrice => Price * Quantity;
    }
}
