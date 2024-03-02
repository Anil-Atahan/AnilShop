using AnilShop.OrderProcessing.Contracts;
using AnilShop.SharedKernel.Abstractions;
using MediatR;
using Serilog;

namespace AnilShop.OrderProcessing.Integrations;

internal class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<OrderDetailResponse>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger _logger;

    public CreateOrderCommandHandler(IOrderRepository orderRepository,
        ILogger logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task<Result<OrderDetailResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var items = request.OrderItems.Select(oi => new OrderItem(
            oi.ProductId, oi.Quantity, oi.UnitPrice, oi.Description));

        var billingAddress = new Address("","","","","","");
        var shippingAddress = billingAddress;
        var newOrder = Order.Factory.Create(request.UserId, shippingAddress, billingAddress, items);

        await _orderRepository.AddAsync(newOrder);
        await _orderRepository.SaveChangesAsync();
        
        _logger.Information("New Order Created! {orderId}", newOrder.Id);

        return new OrderDetailResponse(newOrder.Id);
    }
}