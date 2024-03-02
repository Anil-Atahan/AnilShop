using System.Security.Claims;
using AnilShop.Users.UseCases.AddAddress;
using FastEndpoints;
using MediatR;

namespace AnilShop.Users.Endpoints.AddAddress;

internal sealed class AddAddress : Endpoint<AddAddressRequest>
{
    private readonly IMediator _mediator;

    public AddAddress(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("/users/address");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(AddAddressRequest request,
        CancellationToken ct = default)
    {
        var emailAddress = User.FindFirstValue("EmailAddress");

        var command = new AddAddressToUserCommand(
            emailAddress!,
            request.Street1,
            request.Street2,
            request.City,
            request.State,
            request.PostalCode,
            request.Country);

        var result = await _mediator.Send(command);

        if (result.IsFailure)
        {
            await SendErrorsAsync();
        }
        else
        {
            await SendOkAsync();
        }
    }
}