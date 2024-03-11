using System.Linq;
using System.Threading;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Store.Application.Models;
using Store.Application.Products.Dtos;
using Store.Application.Products.Errors;
using Store.Application.Products.QueryHandlers;
using Store.Domain;
using Store.Domain.Entities;
using Xunit;

namespace Store.Application.UnitTest;

public class GetProductByIdTests : TestDatabaseFixture
{
    [Fact]
    public async void GivenDBIsEmpty_WhenGetProductById__ThenShouldReturnNotFound()
    {
        var context = CreateContext();
        var memoryCache = new MemoryCache(new MemoryCacheOptions());

        var mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Product, GetProductByIdResponse>();
        }).CreateMapper();

        var productQueryHandler = new GetProductByIdQueryHandler(mapper, context, memoryCache, new CacheSettings(){SlidingExpirationInMinutes = 5});

        var result = await productQueryHandler.Handle(new GetProductByIdQuery(1), CancellationToken.None);

        result.Should().NotBeNull();
        result.IsError.Should().BeTrue();
        result.Data.Should().BeNull();
        
        result.Errors.FirstOrDefault().Should().Be(ProductErrors.NotFound);

    }

    [Fact]
    public async void WhenGetProductById_ThenShouldSucceed()
    {
        var context = CreateContext();
        var memoryCache = new MemoryCache(new MemoryCacheOptions());

        var mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Product, GetProductByIdResponse>();
        }).CreateMapper();

        context.Products.Add(Product.Create("گوشی گلکسی اس 22", 10, 100000, 1));

        await context.SaveChangesAsync(CancellationToken.None);

        var productQueryHandler = new GetProductByIdQueryHandler(mapper, context, memoryCache, new CacheSettings() { SlidingExpirationInMinutes = 5 });

        var result = await productQueryHandler.Handle(new GetProductByIdQuery(1), CancellationToken.None);

        result.Should().NotBeNull();
        result.IsError.Should().BeFalse();
        result.Should().BeOfType<Result<GetProductByIdResponse>>();
        result.Data.Should().NotBeNull();
        result.Data!.Title.Should().Be("گوشی گلکسی اس 22");
    }
}
