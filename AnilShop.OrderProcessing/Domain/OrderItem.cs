﻿namespace AnilShop.OrderProcessing.Domain;

internal class OrderItem
{
    public OrderItem(Guid productId, int quantity, decimal unitPrice, string title, string description)
    {
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Title = title;
        Description = description;
    }
    
    private OrderItem()
    {
        
    }
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
}