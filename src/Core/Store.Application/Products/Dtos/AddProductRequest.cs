namespace Store.Application.Products.Dtos
{
    public class AddProductRequest
    {
        public string Title { get; set; }

        public int InventoryCount { get; set; }

        public decimal Price { get; set; }

        public int? Discount { get; set; }
    }
}
