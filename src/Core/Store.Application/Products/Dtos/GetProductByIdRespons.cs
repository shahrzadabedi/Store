namespace Store.Application.Products.Dtos;

public class GetProductByIdResponse
{
    public long Id { get; set; }

    public string Title { get; set; }

    public int InventoryCount { get; set; }

    public decimal Price { get; set; }

    public int? Discount { get; set; }
}
