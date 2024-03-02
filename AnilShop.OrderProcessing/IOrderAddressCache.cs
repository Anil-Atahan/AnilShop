using AnilShop.SharedKernel.Abstractions;

namespace AnilShop.OrderProcessing;

internal interface IOrderAddressCache
{
    Task<Result<OrderAddress>> GetByIdAsync(Guid addressId);
    Task<Result> StoreAsync(OrderAddress orderAddress);
}