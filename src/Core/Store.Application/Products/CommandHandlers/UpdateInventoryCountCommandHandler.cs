
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Models;
using Store.Application.Products.Dtos;
using Store.Application.Products.Errors;
using Store.Domain.Exceptions;
using Store.Infrastructure;

namespace Store.Application.Products.CommandHandlers;

[AutoMap(typeof(AddProductRequest))]
public sealed record UpdateInventoryCountCommand : IRequest<Result<bool>>
{
    public long ProductId { get; set; }

    public int Difference { get; set; }
}

public class UpdateInventoryCountCommandHandler : IRequestHandler<UpdateInventoryCountCommand, Result<bool>>
{
    private readonly DataContext _context;

    public UpdateInventoryCountCommandHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(UpdateInventoryCountCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var product =
                await _context.Products.FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);

            if (product == null)
            {
                return Result<bool>.Error(ProductErrors.NotFound);
            }

            product.UpdateInventory(request.Difference);

            return Result<bool>.Success(true);
        }
        catch (InventoryCountDifferenceInvalidException exception)
        {
            return Result<bool>.ValidationError(exception.ValidationErrors);
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
