namespace AnilShop.Users;

internal class CartItem
{
    // TODO: Add guard clauses
    public CartItem(Guid productId, string description, string title, int quantity, decimal unitPrice)
    {
        ProductId = productId;
        Title = title;
        Description = description;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    // EF
    public CartItem()
    {
        
    }

    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid ProductId { get; private set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }

    internal void UpdateQuantity(int quantity)
    {
        Quantity = quantity;
    }

    public void UpdateTitle(string title)
    {
        Title = title;
    }

    public void UpdateDescription(string description)
    {
        Description = description;
    }

    public void UpdateUnitPrice(decimal unitPrice)
    {
        UnitPrice = unitPrice;
    }
}