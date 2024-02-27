namespace AnilShop.Users.CartEndpoints.AddItem;

public record AddCartItemRequest(Guid ProductId, int Quantity);