using AnilShop.Products.Domain;
using Microsoft.EntityFrameworkCore;

namespace AnilShop.Products.Data;

internal class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _dbContext;

    public ProductRepository(ProductDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Products.FindAsync(id);
    }

    public async Task<List<Product>> ListAsync()
    {
        return await _dbContext.Products.ToListAsync();
    }

    public Task AddAsync(Product product)
    {
        _dbContext.Add(product);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Product product)
    {
        _dbContext.Remove(product);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}