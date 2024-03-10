using Store.Domain.Primitives;

namespace Store.Domain.Entities;

public class Order : Entity
{
    private Order() { }

    public long ProductId { get; private set; }

    public Product Product { get; private set; }

    public DateTime CreationDate { get; private set; }

    public long BuyerId { get; private set; }

    public User Buyer { get; private set; }

    public static Order Create(long productId, DateTime createDate)
    {
        var order = new Order()
        {
            ProductId = productId,
            CreationDate = createDate
        };

        return order;
    }
}


