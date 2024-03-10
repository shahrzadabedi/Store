using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Models;
using Store.Application.Orders.Dtos;
using Store.Application.Products.Errors;
using Store.Domain.Entities;
using Store.Infrastructure;

namespace Store.Application.Orders.CommandHandlers;

[AutoMap(typeof(BuyProductRequest))]
public sealed record BuyProductCommand(
    long UserId,
    long ProductId
) : IRequest<Result<bool>>;

public class BuyCommandHandler : IRequestHandler<BuyProductCommand, Result<bool>>
{
    private readonly DataContext _context;

    public BuyCommandHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(BuyProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);

            if (product == null)
            {
                return Result<bool>.Error(ProductErrors.NotFound);
            }

            var order = Order.Create(request.ProductId, DateTime.Now);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user == null)
            {
                return Result<bool>.Error(ProductErrors.NotFound);
            }

            user.AddOrder(order);

            product.DecreaseInventory();

            _context.Orders.Add(order);

            await _context.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
        catch (Exception exception)
        {
            return Result<bool>.Error(exception);
        }
    }
}
