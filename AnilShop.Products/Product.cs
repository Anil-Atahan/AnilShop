namespace AnilShop.Products;

internal class Product
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; set; }

    internal Product(Guid id, string title, string description, decimal price)
    {
        // TODO: ADD GUARD CLAUSES
        Id = id;
        Title = title;
        Description = description;
        Price = price;
    }

    internal void UpdatePrice(decimal newPrice)
    {
        Price = newPrice;
    }
}