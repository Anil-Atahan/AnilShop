namespace AnilShop.Products.Contracts;

public record ProductDetailsResponse(Guid ProductId, string Title,
    string Description, decimal Price);