using AnilShop.Products.Contracts;
using AnilShop.Products.Errors;
using AnilShop.SharedKernel;
using MediatR;

namespace AnilShop.Products.Integrations;

internal class ProductDetailsQueryHandler : IRequestHandler<ProductDetailsQuery, Result<ProductDetailsResponse>>
{
    private readonly IProductService _productService;

    public ProductDetailsQueryHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<Result<ProductDetailsResponse>> Handle(ProductDetailsQuery request, CancellationToken cancellationToken)
    {
        var product = await _productService.GetProductByIdAsync(request.ProductId);

        if (product is null)
        {
            return Result.Failure<ProductDetailsResponse>(ProductErrors.NotFound);
        }

        var response = new ProductDetailsResponse(product.Id, product.Title, product.Description, product.Price);

        return response;
    }
}