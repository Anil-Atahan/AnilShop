namespace AnilShop.Products;

internal interface IReadOnlyProductRepository
{
    Task<Product?> GetByIdAsync(Guid id);
    Task<List<Product>> ListAsync();
}