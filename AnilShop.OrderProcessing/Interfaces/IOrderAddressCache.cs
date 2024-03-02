using AnilShop.OrderProcessing.Infrastructure;
using AnilShop.SharedKernel;

namespace AnilShop.OrderProcessing.Interfaces;

internal interface IOrderAddressCache
{
    Task<Result<OrderAddress>> GetByIdAsync(Guid addressId);
    Task<Result> StoreAsync(OrderAddress orderAddress);
}