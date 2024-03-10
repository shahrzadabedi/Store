using Store.Application.Enums;

namespace Store.Application.Models;

public class Error
{
    public string Code { get; set; }
    public HttpErrorCode HttpCode { get; set; }
    public string? Message { get; set; }
}
