using FastEndpoints;

namespace AnilShop.Products.Endpoints.Update;

internal class UpdatePrice(IProductService productService) :
    Endpoint<UpdateProductPriceRequest, ProductDto>
{
    private readonly IProductService _productService = productService;

    public override void Configure()
    {      
        Patch("/products/{id}/update-price");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateProductPriceRequest req, CancellationToken cancellationToken = default)
    {
        await _productService.UpdateProductPriceAsync(req.Id, req.NewPrice);
        
        var updatedProduct = await _productService.GetProductByIdAsync(req.Id);

        await SendAsync(updatedProduct);
    }
}