using FluentValidation;
using Store.Domain.Entities;

namespace Store.Domain.Validators;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Product title cannot be empty");

        RuleFor(x => x.Title)
            .MaximumLength(40)
            .WithMessage("Product title should be less than or equal to 40 characters");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0");

        RuleFor(x => x.Discount)
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0");

        RuleFor(x => x.InventoryCount)
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0");
    }
}
