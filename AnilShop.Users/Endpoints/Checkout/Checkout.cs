using System.Security.Claims;
using AnilShop.Users.UseCases.Checkout;
using FastEndpoints;
using MediatR;

namespace AnilShop.Users.Endpoints.Checkout;

internal class Checkout : Endpoint<CheckoutRequest, CheckoutResponse>
{
    private readonly IMediator _mediator;

    public Checkout(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("/cart/checkout");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(CheckoutRequest request,
        CancellationToken ct = default)
    {
        var emailAddress = User.FindFirstValue("EmailAddress");

        var command = new CheckoutCartCommand(emailAddress,
            request.ShippingAddressId, request.BillingAddressId);

        var result = await _mediator.Send(command);

        if (result.IsFailure)
        {
            await SendErrorsAsync();
        }
        else
        {
            await SendOkAsync(new CheckoutResponse(result.Value));
        }
    }
}