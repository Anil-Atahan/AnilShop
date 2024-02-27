namespace AnilShop.Users.CartEndpoints.ListItems;

public class CartResponse
{
    public List<CartItemDto> CartItems { get; set; } = new();
}