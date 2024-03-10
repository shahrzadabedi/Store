namespace Store.Presentation.Contracts;

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string StatusPhrase { get; set; }
    public List<ErrorItem> Errors { get; } = new();
    public DateTime Timestamp { get; set; }
}

public record ErrorItem(string Code, string Message);
