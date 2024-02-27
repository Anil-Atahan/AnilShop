namespace AnilShop.Users.CartEndpoints.ListItems;

public record CartItemDto(Guid Id, Guid ProductId, string Description, int Quantity, decimal UnitPrice);