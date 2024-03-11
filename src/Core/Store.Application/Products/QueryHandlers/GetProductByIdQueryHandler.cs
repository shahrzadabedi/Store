using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Store.Application.Models;
using Store.Application.Products.Dtos;
using Store.Application.Products.Errors;
using Store.Domain;
using Store.Infrastructure;

namespace Store.Application.Products.QueryHandlers;

public sealed record GetProductByIdQuery(long Id) :
    IRequest<Result<GetProductByIdResponse>>;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<GetProductByIdResponse>>
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;
    private readonly IMemoryCache _memoryCache;
    private readonly float _slidingExpirationInMinutes;

    private const string CacheKeyPrefix = "GetProduct";

    public GetProductByIdQueryHandler(IMapper mapper,
        DataContext context, IMemoryCache memoryCache, CacheSettings cacheSettings)
    {
        _context = context;
        _mapper = mapper;
        _memoryCache = memoryCache;
        _slidingExpirationInMinutes = cacheSettings.SlidingExpirationInMinutes;
    }

    public async Task<Result<GetProductByIdResponse>> Handle(GetProductByIdQuery request,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"{CacheKeyPrefix}-{request.Id}";

        var response = GetProductFromCache(cacheKey);

        if (response == null)
        {
            response = await _context.Products
                .AsNoTracking()
                .ProjectTo<GetProductByIdResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (response == null)
                return Result<GetProductByIdResponse>.Error(ProductErrors.NotFound);

            SetProductInCache(cacheKey, response);
        }

        return Result<GetProductByIdResponse>.Success(response);
    }

    private GetProductByIdResponse? GetProductFromCache(string cacheKey)
    {
        GetProductByIdResponse? result = null;
        if (_memoryCache.TryGetValue(cacheKey, out GetProductByIdResponse? data))
        {
            result = data;
        }

        return result;
    }

    private void SetProductInCache(string cacheKey, GetProductByIdResponse value)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(_slidingExpirationInMinutes));

        _memoryCache.Set(cacheKey, value, cacheEntryOptions);
    }
}
