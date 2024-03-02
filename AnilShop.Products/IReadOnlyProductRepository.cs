using AnilShop.Products.Domain;

namespace AnilShop.Products;

internal interface IReadOnlyProductRepository
{
    Task<Product?> GetByIdAsync(Guid id);
    Task<List<Product>> ListAsync();
}