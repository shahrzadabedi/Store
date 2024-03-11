using FluentValidation.Results;
using Store.Domain.Exceptions;
using Store.Domain.Primitives;
using Store.Domain.Validators;

namespace Store.Domain.Entities;

public class Product : Entity
{
    private Product() { }

    public string Title { get; private set; }

    public int InventoryCount { get; private set; }

    public decimal Price { get; private set; }

    public int? Discount { get; private set; }

    public decimal DiscountedPrice { get { return Price * (1 - (decimal)(Discount ?? 0) / 100); } }

    public static Product Create(string title, int inventoryCount, decimal price, int? discount)
    {
        var product = new Product()
        {
            Title = title,
            InventoryCount = inventoryCount,
            Price = price,
            Discount = discount,
        };

        var validator = new ProductValidator();

        var validationResult = validator.Validate(product);

        if (validationResult.IsValid)
            return product;

        HandleValidationResult(validationResult);

        return new Product();
    }

    public decimal GetDiscountedPrice() => Price * (1 - (Discount ?? 0) / 100);

    public void IncreaseInventory(int difference)
    {
        if (difference <= 0)
            throw new InventoryCountDifferenceInvalidException();

        InventoryCount += difference;
    }

    public void DecreaseInventory() => InventoryCount -= 1;

    private static void HandleValidationResult(ValidationResult validationResult)
    {
        var exception = new ProductNotValidException("Product is not valid");

        validationResult.Errors.ForEach(v => exception.ValidationErrors.Add(v.ErrorMessage));

        throw exception;
    }
}
