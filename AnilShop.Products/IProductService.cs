namespace AnilShop.Products;

internal interface IProductService
{
    Task<List<ProductDto>> ListProductsAsync();
    Task<ProductDto?> GetProductByIdAsync(Guid id);
    Task CreateProductAsync(ProductDto newProduct);
    Task DeleteProductAsync(Guid id);
    Task UpdateProductPriceAsync(Guid productId, decimal newPrice);
}