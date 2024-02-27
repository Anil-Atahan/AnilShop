using FastEndpoints;

namespace AnilShop.Products.Endpoints.Delete;

internal class Delete(IProductService productService) :
    Endpoint<DeleteProductRequest>
{
    private readonly IProductService _productService = productService;

    public override void Configure()
    {      
        Delete("/products/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteProductRequest req, CancellationToken cancellationToken = default)
    {
        await _productService.DeleteProductAsync(req.Id);

        await SendNoContentAsync();
    }
}