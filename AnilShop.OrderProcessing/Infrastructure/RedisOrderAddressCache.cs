using System.Text.Json;
using AnilShop.OrderProcessing.Interfaces;
using AnilShop.SharedKernel;
using Serilog;
using StackExchange.Redis;

namespace AnilShop.OrderProcessing.Infrastructure;

internal class RedisOrderAddressCache : IOrderAddressCache
{
    private readonly IDatabase _db;
    private readonly ILogger _logger;

    public RedisOrderAddressCache(ILogger logger)
    {
        // TODO: Get From Config
        var redis = ConnectionMultiplexer.Connect("localhost");
        _db = redis.GetDatabase();
        _logger = logger;
    }

    public async Task<Result<OrderAddress>> GetByIdAsync(Guid addressId)
    {
        string? fetchedJson = await _db.StringGetAsync(addressId.ToString());

        if (fetchedJson is null)
        {
            _logger.Warning("Address {id} not found in {db}", addressId, "REDIS");
            return Result.Failure<OrderAddress>(Error.None);
        }

        var address = JsonSerializer.Deserialize<OrderAddress>(fetchedJson);

        if (address is null)
        {
            return Result.Failure<OrderAddress>(Error.None);
        }
        
        _logger.Information("Address {id} returned from {db}", addressId, "REDIS");
        return Result.Success(address);
    }

    public async Task<Result> StoreAsync(OrderAddress orderAddress)
    {
        var key = orderAddress.Id.ToString();
        var addressJson = JsonSerializer.Serialize(orderAddress);

        await _db.StringSetAsync(key, addressJson);
        _logger.Information("Address {id} stored in {db}", orderAddress.Id, "REDIS");

        return Result.Success();
    }
}