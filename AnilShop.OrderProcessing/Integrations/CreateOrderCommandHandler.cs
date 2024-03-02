using AnilShop.OrderProcessing.Contracts;
using AnilShop.OrderProcessing.Domain;
using AnilShop.OrderProcessing.Interfaces;
using AnilShop.SharedKernel;
using MediatR;
using Serilog;

namespace AnilShop.OrderProcessing.Integrations;

internal class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<OrderDetailResponse>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger _logger;
    private readonly IOrderAddressCache _orderAddressCache;

    public CreateOrderCommandHandler(IOrderRepository orderRepository,
        ILogger logger, 
        IOrderAddressCache orderAddressCache)
    {
        _orderRepository = orderRepository;
        _logger = logger;
        _orderAddressCache = orderAddressCache;
    }

    public async Task<Result<OrderDetailResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var items = request.OrderItems.Select(oi => new OrderItem(
            oi.ProductId, oi.Quantity, oi.UnitPrice, oi.Description));

        var shippingAddress = await _orderAddressCache.GetByIdAsync(request.ShippingAddressId);
        var billingAddress = await _orderAddressCache.GetByIdAsync(request.BillingAddressId);

        var newOrder = Order.Factory.Create(
            request.UserId,
            shippingAddress.Value.Address, 
            billingAddress.Value.Address, 
            items);

        await _orderRepository.AddAsync(newOrder);
        await _orderRepository.SaveChangesAsync();
        
        _logger.Information("New Order Created! {orderId}", newOrder.Id);

        return new OrderDetailResponse(newOrder.Id);
    }
}