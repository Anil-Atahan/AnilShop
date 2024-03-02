using AnilShop.Users.Contracts;
using MediatR;
using Serilog;

namespace AnilShop.Users.Integrations;

internal class UserAddressIntegrationEventDispatcherHandler
    : INotificationHandler<AddressAddedEvent>
{
    private readonly ILogger _logger;
    private readonly IMediator _mediator;

    public UserAddressIntegrationEventDispatcherHandler(ILogger logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Handle(AddressAddedEvent notification, CancellationToken cancellationToken)
    {
        Guid userId = Guid.Parse(notification.NewAddress.UserId);

        var addressDetails = new UserAddressDetails(
            userId,
            notification.NewAddress.Id,
            notification.NewAddress.StreetAddress.Street1,
            notification.NewAddress.StreetAddress.Street2,
            notification.NewAddress.StreetAddress.City,
            notification.NewAddress.StreetAddress.State,
            notification.NewAddress.StreetAddress.PostalCode,
            notification.NewAddress.StreetAddress.Country);

        await _mediator!.Publish(new NewUserAddressAddedIntegrationEvent(addressDetails));
        
        _logger.Information("[DE Handler] New address integration event sent for {user}. Address: {address}",
            notification.NewAddress.UserId,
            notification.NewAddress.StreetAddress);
    }
}