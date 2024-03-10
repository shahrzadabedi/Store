namespace Store.Domain.Primitives;

public abstract class Entity
{
    public long Id { get; protected set; }

    protected Entity(){}
}
