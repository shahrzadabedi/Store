namespace Store.Domain.Exceptions;

public class InventoryCountDifferenceInvalidException : DomainModelInvalidException
{
    public InventoryCountDifferenceInvalidException() : base("Inventory count difference must be greater than zero")
    {
    }

    public InventoryCountDifferenceInvalidException(string message) : base(message) { }

    public InventoryCountDifferenceInvalidException(string message, Exception inner) : base(message, inner) { }
}
