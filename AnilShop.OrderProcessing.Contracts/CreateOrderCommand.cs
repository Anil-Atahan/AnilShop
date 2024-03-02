using AnilShop.SharedKernel;
using MediatR;

namespace AnilShop.OrderProcessing.Contracts;

public record CreateOrderCommand(Guid UserId,
    Guid ShippingAddressId,
    Guid BillingAddressId,
    List<OrderItemDetails> OrderItems) : IRequest<Result<OrderDetailResponse>>;