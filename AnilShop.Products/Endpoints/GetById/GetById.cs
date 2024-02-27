using FastEndpoints;

namespace AnilShop.Products.Endpoints.GetById;

internal class GetById(IProductService productService) :
    Endpoint<GetProductByIdRequest, ProductDto>
{
    private readonly IProductService _productService = productService;

    public override void Configure()
    {
        Get("/products/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetProductByIdRequest req, CancellationToken cancellationToken = default)
    {
        var product = await _productService.GetProductByIdAsync(req.Id);

        if (product is null)
        {
            await SendNotFoundAsync();
            return;
        }
        await SendAsync(product);
    }
}