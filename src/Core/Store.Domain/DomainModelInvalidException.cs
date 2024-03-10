namespace Store.Domain;

public abstract class DomainModelInvalidException : Exception
{
    private protected DomainModelInvalidException()
    {
        ValidationErrors = new List<string>();
    }

    private protected DomainModelInvalidException(string message) : base(message)
    {
        ValidationErrors = new List<string>();
    }

    private protected DomainModelInvalidException(string message, Exception inner) : base(message, inner)
    {
        ValidationErrors = new List<string>();
    }

    public List<string> ValidationErrors { get; }
}
