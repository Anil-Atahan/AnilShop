using Microsoft.AspNetCore.Identity;

namespace AnilShop.Users;

internal class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    private readonly List<CartItem> _cartItems = new();
    public IReadOnlyCollection<CartItem> CartItems => _cartItems.AsReadOnly();

    private readonly List<UserStreetAddress> _addresses = new();
    public IReadOnlyCollection<UserStreetAddress> Addresses => _addresses.AsReadOnly();

    internal void AddItemToCart(CartItem item)
    {
        var existingProduct = _cartItems.SingleOrDefault(c => c.ProductId == item.ProductId);
        if (existingProduct is not null)
        {
            existingProduct.UpdateQuantity(existingProduct.Quantity + item.Quantity);
            existingProduct.UpdateTitle(existingProduct.Title);
            existingProduct.UpdateDescription(existingProduct.Description);
            existingProduct.UpdateUnitPrice(existingProduct.UnitPrice);
            return;
        }

        _cartItems.Add(item);
    }
    
    internal UserStreetAddress AddAddress(Address address)
    {
        var existingAddress = _addresses.SingleOrDefault(a => a.StreetAddress == address);
        if (existingAddress is not null)
        {
            return existingAddress;
        }

        var newAddress = new UserStreetAddress(Id, address);
        _addresses.Add(newAddress);

        return newAddress;
    }

    internal void ClearCart()
    {
        _cartItems.Clear();
    }
}