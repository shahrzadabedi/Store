using Store.Domain.Primitives;

namespace Store.Domain.Entities;
public class User : Entity
{
    public string Name { get; private set; }

    private readonly List<Order> _orders = new();

    public IReadOnlyCollection<Order> Orders => _orders;

    public User(string name)
    {
        Name = name;
    }
}


