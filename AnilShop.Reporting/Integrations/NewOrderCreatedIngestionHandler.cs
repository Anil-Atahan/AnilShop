using AnilShop.OrderProcessing.Contracts;
using AnilShop.Products.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AnilShop.Reporting.Integrations;

internal class NewOrderCreatedIngestionHandler : 
    INotificationHandler<OrderCreatedIntegrationEvent>
{
    private readonly ILogger<NewOrderCreatedIngestionHandler> _logger;
    private readonly OrderIngestionService _orderIngestionService;
    private readonly IMediator _mediator;

    public NewOrderCreatedIngestionHandler(ILogger<NewOrderCreatedIngestionHandler> logger,
        OrderIngestionService orderIngestionService,
        IMediator mediator)
    {
        _logger = logger;
        _orderIngestionService = orderIngestionService;
        _mediator = mediator;
    }

    public async Task Handle(OrderCreatedIntegrationEvent notification,
        CancellationToken ct)
    {
        _logger.LogInformation("Handling order created event to populate reporting database...");

        var orderItems = notification.OrderDetails.OrderItems;
        int year = notification.OrderDetails.DateCreated.Year;
        int month = notification.OrderDetails.DateCreated.Month;

        foreach (var item in orderItems)
        {
            // look up book details to get author and title
            // TODO: Implement Materialized View or other cache
            var bookDetailsQuery = new ProductDetailsQuery(item.ProductId);
            var result = await _mediator.Send(bookDetailsQuery);

            if (!result.IsSuccess)
            {
                _logger.LogWarning("Issue loading book details for {id}", item.ProductId);
                continue;
            }

            string title = result.Value.Title;

            var sale = new BookSale
            {
                BookId = item.ProductId,
                Month = month,
                Title = title,
                Year = year,
                TotalSales = item.Quantity * item.UnitPrice,
                UnitsSold = item.Quantity
            };

            await _orderIngestionService.AddOrUpdateMonthlyBookSalesAsync(sale);
        }
    }
}
