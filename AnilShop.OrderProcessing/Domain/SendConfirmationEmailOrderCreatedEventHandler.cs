using AnilShop.EmailSending.Conracts;
using AnilShop.Users.Contracts;
using MediatR;
using Serilog;

namespace AnilShop.OrderProcessing.Domain;

internal class SendConfirmationEmailOrderCreatedEventHandler :
    INotificationHandler<OrderCreatedEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;

    public SendConfirmationEmailOrderCreatedEventHandler(IMediator mediator, ILogger logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        var userByIdQuery = new UserDetailsByIdQuery(notification.Order.UserId);

        var result = await _mediator.Send(userByIdQuery);

        if (result.IsFailure)
        {
            _logger.Error($"[SendConfirmationEmailOrderCreatedEventHandler] =>" +
                          $"Error while getting user info. For details: {result.Error}");
        }

        var command = new SendEmailCommand()
        {
            To = result.Value.EmailAddress,
            From = "noreply@test.com",
            Subject = "Your AnilShop Purchase",
            Body = $"You bought {notification.Order.OrderItems.Count} items."
        };

        await _mediator.Send(command);
    }
}