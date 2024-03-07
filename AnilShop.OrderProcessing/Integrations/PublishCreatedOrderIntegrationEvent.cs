using AnilShop.OrderProcessing.Contracts;
using AnilShop.OrderProcessing.Domain;
using MediatR;

namespace AnilShop.OrderProcessing.Integrations;

internal class PublishCreatedOrderIntegrationEvent :
    INotificationHandler<OrderCreatedEvent>
{
    private readonly IMediator _mediator;

    public PublishCreatedOrderIntegrationEvent(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        var dto = new OrderDetailsDto
        {
            OrderId = notification.Order.Id,
            UserId = notification.Order.UserId,
            DateCreated = notification.Order.DateCreated,
            OrderItems = notification.Order.OrderItems
                .Select(oi => new OrderItemDetails(oi.ProductId, oi.Title, oi.Quantity, oi.UnitPrice, oi.Description)).ToList()
        };

        var integrationEvent = new OrderCreatedIntegrationEvent(dto);

        await _mediator.Publish(integrationEvent);
    }
}