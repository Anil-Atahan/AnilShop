using AnilShop.OrderProcessing.Domain;
using AnilShop.OrderProcessing.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AnilShop.OrderProcessing.Infrastructure.Data;

internal class OrderRepository : IOrderRepository
{
    private readonly OrderProcessingDbContext _dbContext;
    
    public OrderRepository(OrderProcessingDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Order>> ListAsync()
    {
        return await _dbContext.Orders
            .Include(o => o.OrderItems)
            .ToListAsync();
    }

    public async Task AddAsync(Order order)
    {
        await _dbContext.Orders.AddAsync(order);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}