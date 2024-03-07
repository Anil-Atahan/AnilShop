using MediatR;

namespace AnilShop.OrderProcessing.Contracts;

public class OrderCreatedIntegrationEvent : INotification
{
    public DateTimeOffset DateCreated { get; private set; } = DateTimeOffset.UtcNow;
    public OrderDetailsDto OrderDetails { get; private set; }

    public OrderCreatedIntegrationEvent(OrderDetailsDto orderDetails)
    {
        OrderDetails = orderDetails;
    }
}