using AnilShop.Users.Contracts;
using MediatR;
using Serilog;

namespace AnilShop.OrderProcessing.Integrations;

internal class AddressCacheUpdatingNewUserAddressHandler
    : INotificationHandler<NewUserAddressAddedIntegrationEvent>
{
    private readonly IOrderAddressCache _orderAddressCache;
    private readonly ILogger _logger;

    public AddressCacheUpdatingNewUserAddressHandler(IOrderAddressCache orderAddressCache, ILogger logger)
    {
        _orderAddressCache = orderAddressCache;
        _logger = logger;
    }

    public async Task Handle(NewUserAddressAddedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var orderAddress = new OrderAddress(notification.Details.AddressId,
            new Address(notification.Details.Street1,
                notification.Details.Street2,
                notification.Details.City,
                notification.Details.State,
                notification.Details.PostalCode,
                notification.Details.Country));

        await _orderAddressCache.StoreAsync(orderAddress);
        
        _logger.Information("Cached updated with new address {address}", orderAddress);
    }
}