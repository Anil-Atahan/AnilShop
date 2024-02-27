namespace AnilShop.Products;

internal interface IProductRepository : IReadOnlyProductRepository
{
    Task AddAsync(Product product);
    Task DeleteAsync(Product product);
    Task SaveChangesAsync();
}