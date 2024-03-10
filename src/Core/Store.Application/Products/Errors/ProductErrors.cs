using Store.Application.Enums;
using Store.Application.Models;

namespace Store.Application.Products.Errors;

public static class ProductErrors
{
    public static readonly Error NotFound = new()
    {
        HttpCode = HttpErrorCode.NotFound,
        Code = nameof(NotFound),
        Message = "Product not found"
    };
}
