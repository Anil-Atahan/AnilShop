using AnilShop.OrderProcessing.Domain;

namespace AnilShop.OrderProcessing.Interfaces;

internal interface IOrderRepository
{
    Task<List<Order>> ListAsync();
    Task AddAsync(Order order);
    Task SaveChangesAsync();
}