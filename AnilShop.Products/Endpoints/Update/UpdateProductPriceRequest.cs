namespace AnilShop.Products.Endpoints.Update;

public record UpdateProductPriceRequest(Guid Id, decimal NewPrice);