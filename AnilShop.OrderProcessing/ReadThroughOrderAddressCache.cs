using AnilShop.SharedKernel.Abstractions;
using AnilShop.Users.Contracts;
using MediatR;
using Serilog;

namespace AnilShop.OrderProcessing;

internal class ReadThroughOrderAddressCache : IOrderAddressCache
{
    private readonly RedisOrderAddressCache _redisCache;
    private readonly IMediator _mediator;
    private readonly ILogger _logger;

    public ReadThroughOrderAddressCache(RedisOrderAddressCache redisCache, IMediator mediator, ILogger logger)
    {
        _redisCache = redisCache;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task<Result<OrderAddress>> GetByIdAsync(Guid addressId)
    {
        var result = await _redisCache.GetByIdAsync(addressId);
        if (result.IsSuccess) return result;

        _logger.Information("Address {id} not found; fetching from source.", addressId);
        var query = new UserAddressDetailsByIdQuery(addressId);

        var queryResult = await _mediator.Send(query);

        if (queryResult.IsSuccess)
        {
            var dto = queryResult.Value;
            var address = new Address(dto.Street1,
                dto.Street2,
                dto.City,
                dto.State,
                dto.PostalCode,
                dto.Country);

            var orderAddress = new OrderAddress(dto.AddressId, address);
            await StoreAsync(orderAddress);
            return orderAddress;
        }

        return Result.Failure<OrderAddress>(Error.None);
    }

    public Task<Result> StoreAsync(OrderAddress orderAddress)
    {
        return _redisCache.StoreAsync(orderAddress);
    }
}