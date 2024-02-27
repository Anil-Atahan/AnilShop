namespace AnilShop.Products;

internal class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<List<ProductDto>> ListProductsAsync()
    {
        var products = (await _productRepository.ListAsync())
            .Select(product => new ProductDto(product.Id, product.Title, product.Description, product.Price))
            .ToList();
        return products;
    }

    public async Task<ProductDto?> GetProductByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        return new ProductDto(product!.Id, product.Title, product.Description, product.Price);
    }

    public async Task CreateProductAsync(ProductDto newProduct)
    {
        var product = new Product(newProduct.Id, newProduct.Title, newProduct.Description, newProduct.Price);
        
        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product is not null)
        {
            await _productRepository.DeleteAsync(product);
            await _productRepository.SaveChangesAsync();
        }
    }

    public async Task UpdateProductPriceAsync(Guid productId, decimal newPrice)
    {
        var product = await _productRepository.GetByIdAsync(productId);

        product!.UpdatePrice(newPrice);
        await _productRepository.SaveChangesAsync();
    }
}