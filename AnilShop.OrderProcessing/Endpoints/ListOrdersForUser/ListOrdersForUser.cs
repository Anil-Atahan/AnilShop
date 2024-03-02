using System.Security.Claims;
using FastEndpoints;
using MediatR;

namespace AnilShop.OrderProcessing.Endpoints.ListOrdersForUser;

public class ListOrdersForUser : EndpointWithoutRequest<ListOrdersForUserResponse>
{
    private readonly IMediator _mediator;

    public ListOrdersForUser(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("/orders");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(CancellationToken ct = default)
    {
        var emailAddress = User.FindFirstValue("EmailAddress");

        var query = new ListOrdersForUserQuery(emailAddress!);

        var result = await _mediator.Send(query, ct);

        if (result.IsFailure)
        {
            await SendErrorsAsync();
        }

        var response = new ListOrdersForUserResponse
        {
            Orders = result.Value.Select(o => new OrderSummary
            {
                DateCreated = o.DateCreated,
                DateShipped = o.DateShipped,
                Total = o.Total,
                UserId = o.UserId,
                OrderId = o.OrderId
            }).ToList()
        };

        await SendAsync(response);
    }
}