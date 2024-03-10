using System.Linq;
using Store.Domain.Entities;
using System.Threading;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Store.Application.Enums;
using Store.Application.Products.CommandHandlers;
using Xunit;
using Store.Application.Models;

namespace Store.Application.UnitTest;

public class UpdateInventoryCountTests : TestDatabaseFixture
{
    [Fact]
    public async void WhenInventoryCountIsIncremented_ThenShouldSucceed()
    {
        var context = CreateContext();

        context.Products.Add(Product.Create("گوشی گلکسی اس 22", 10, 100000, 1));

        await context.SaveChangesAsync(CancellationToken.None);

        var updateInventoryCountHandler = new UpdateInventoryCountCommandHandler(context);

        var result =
            updateInventoryCountHandler.Handle(new UpdateInventoryCountCommand()
            {
                ProductId = 1,
                Difference = 50
            }, CancellationToken.None);

        result.Result.IsError.Should().BeFalse();
        result.Result.Data.Should().Be(true);

        var afterProduct = await context.Products.FirstOrDefaultAsync(p => p.Id == 1, CancellationToken.None);

        afterProduct!.InventoryCount.Should().Be(60);
    }

    [Fact]
    public async void WhenInventoryCountDifferenceIsNegative_ThenShouldFail()
    {
        var context = CreateContext();

        context.Products.Add(Product.Create("گوشی گلکسی اس 22", 10, 100000, 1));

        await context.SaveChangesAsync(CancellationToken.None);

        var updateInventoryCountHandler = new UpdateInventoryCountCommandHandler(context);

        var result =
            updateInventoryCountHandler.Handle(new UpdateInventoryCountCommand()
            {
                ProductId = 1,
                Difference = -50
            }, CancellationToken.None);

        result.Result.IsError.Should().BeTrue();
        result.Result.Data.Should().Be(false);
        result.Result.Errors.Should().NotBeEmpty();
        result.Result.Errors.FirstOrDefault()!.HttpCode.Should().Be(HttpErrorCode.BadRequest);
        result.Result.Errors.FirstOrDefault()!.Code.Should().Be(ErrorCodeConst.ValidationError);
    }
}
