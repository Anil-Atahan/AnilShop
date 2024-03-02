namespace AnilShop.OrderProcessing.Endpoints.ListOrdersForUser;

public class ListOrdersForUserResponse
{
    public List<OrderSummary> Orders { get; set; } = new();
}