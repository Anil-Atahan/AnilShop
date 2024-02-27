namespace AnilShop.Products.Endpoints.Create;

public record CreateProductRequest(Guid? Id, string Title, string Description, decimal Price);