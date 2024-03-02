using System.Security.Claims;
using AnilShop.Users.UseCases;
using AnilShop.Users.UseCases.AddCartItem;
using FastEndpoints;
using MediatR;

namespace AnilShop.Users.CartEndpoints.AddItem;

internal class AddItem : Endpoint<AddCartItemRequest>
{
    private readonly IMediator _mediator;
    
    public AddItem(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public override void Configure()
    {
        Post("/cart");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(AddCartItemRequest req, CancellationToken cancellationToken = default)
    {
        var emailAddress = User.FindFirstValue("EmailAddress");

        var command = new AddCartItemCommand(req.ProductId, req.Quantity, emailAddress!);

        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            await SendOkAsync();
        }
        else
        {
            await SendErrorsAsync();
        }
    }
}