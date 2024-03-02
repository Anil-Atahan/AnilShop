﻿namespace AnilShop.OrderProcessing.Domain;

internal class OrderItem
{
    public OrderItem(Guid bookId, int quantity, decimal unitPrice, string description)
    {
        BookId = bookId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Description = description;
    }
    
    private OrderItem()
    {
        
    }
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid BookId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public string Description { get; private set; } = string.Empty;
}