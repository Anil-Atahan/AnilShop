using FastEndpoints;

namespace AnilShop.Products.Endpoints.Create;

internal class Create(IProductService productService) :
    Endpoint<CreateProductRequest, ProductDto>
{
    private readonly IProductService _productService = productService;

    public override void Configure()
    {
        Post("/products");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateProductRequest req, CancellationToken cancellationToken = default)
    {
        var newProductDto = new ProductDto(req.Id ?? Guid.NewGuid(),
            req.Title,
            req.Description,
            req.Price);
        
        await _productService.CreateProductAsync(newProductDto);
        
        await SendCreatedAtAsync<GetById.GetById>(new { newProductDto.Id }, newProductDto);
    }
}