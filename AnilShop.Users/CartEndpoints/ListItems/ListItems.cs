using System.Security.Claims;
using AnilShop.Users.UseCases;
using FastEndpoints;
using MediatR;

namespace AnilShop.Users.CartEndpoints.ListItems;

internal class ListItems : EndpointWithoutRequest<CartResponse>
{
    private readonly IMediator _mediator;

    public ListItems(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("/cart");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var emailAddress = User.FindFirstValue("EmailAddress");

        var query = new ListCartItemsQuery(emailAddress!);

        var result = await _mediator.Send(query, ct);

        if (result.IsFailure)
        {
            await SendErrorsAsync();
        }
        else
        {
            var response = new CartResponse()
            {
                CartItems = result.Value
            };
            await SendAsync(response);
        }
    }
}