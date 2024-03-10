using Store.Domain.Primitives;

namespace Store.Domain.Entities;

public class Order : Entity
{
    public long ProductId { get; private set; }

    public Product Product { get; private set; }

    public DateTime CreationDate { get; private set; }

    public long BuyerId { get; private set; }

    public User Buyer { get; private set; }
}


