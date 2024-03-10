namespace Store.Application.Orders.Dtos;

public class BuyProductRequest
{
    public long UserId { get; set; }

    public long ProductId { get; set; }
}
