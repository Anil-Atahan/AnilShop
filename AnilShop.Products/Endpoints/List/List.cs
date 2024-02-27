using FastEndpoints;

namespace AnilShop.Products.Endpoints.List;

internal class List(IProductService productService) :
    EndpointWithoutRequest<ListProductsResponse>
{
    private readonly IProductService _productService = productService;

    public override void Configure()
    {
        Get("/products");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken = default)
    {
        var products = await _productService.ListProductsAsync();
        await SendAsync(new ListProductsResponse
        {
            Products = products
        });
    }
}