using AutoMapper;
using MediatR;
using Store.Application.Models;
using Store.Application.Products.Dtos;
using Store.Domain.Entities;
using Store.Domain.Exceptions;
using Store.Infrastructure;

namespace Store.Application.Products.CommandHandlers;

[AutoMap(typeof(AddProductRequest))]
public sealed record AddProductCommand(
    string Title,
    int InventoryCount,
    decimal Price,
    int? Discount
) : IRequest<Result<bool>>;

public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Result<bool>>
{
    private readonly DataContext _context;

    public AddProductCommandHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var product = Product.Create(request.Title, request.InventoryCount, request.Price, request.Discount);

            _context.Products.Add(product);

            await _context.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
        catch (ProductNotValidException exception)
        {
            return Result<bool>.ValidationError(exception.ValidationErrors);
        }
        catch (Exception exception)
        {
            return Result<bool>.Error(exception);
        }
    }
}
