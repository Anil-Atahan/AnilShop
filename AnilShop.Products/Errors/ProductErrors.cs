using AnilShop.SharedKernel.Abstractions;

namespace AnilShop.Products.Errors;

public static class ProductErrors
{
    public static Error NotFound = new(
        "Product.NotFound",
        "The product with the specified identifier was not found");
}