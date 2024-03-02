namespace AnilShop.OrderProcessing.Contracts;

public record OrderItemDetails(Guid ProductId,
    string Title,
    int Quantity,
    decimal UnitPrice,
    string Description);