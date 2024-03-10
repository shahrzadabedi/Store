namespace Store.Domain.Exceptions;

public class ProductNotValidException : DomainModelInvalidException
{
    public ProductNotValidException() { }

    public ProductNotValidException(string message) : base(message) { }

    public ProductNotValidException(string message, Exception inner) : base(message, inner) { }
}
