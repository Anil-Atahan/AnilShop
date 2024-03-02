using System.Security.Claims;
using AnilShop.Users.UseCases.ListAddresses;
using FastEndpoints;
using MediatR;

namespace AnilShop.Users.Endpoints.ListAddresses;

internal class ListAddresses : EndpointWithoutRequest<AddressListResponse>
{
    private readonly IMediator _mediator;

    public ListAddresses(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("/users/addresses");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(CancellationToken ct = default)
    {
        var emailAddress = User.FindFirstValue("EmailAddress");
        var query = new ListAddressesQuery(emailAddress!);

        var result = await _mediator.Send(query, ct);

        if (result.IsFailure)
        {
            await SendErrorsAsync();
        }
        else
        {
            var response = new AddressListResponse();
            response.Addresses = result.Value;
            await SendOkAsync(response);
        }
    }
}