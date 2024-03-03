using AnilShop.Users.Domain;
using AnilShop.Users.UseCases.CreateUser;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AnilShop.Users.Endpoints.Create;

internal class Create : Endpoint<CreateUserRequest>
{
    private readonly IMediator _mediator;
    public Create(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("/users");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        CreateUserRequest req,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateUserCommand(req.Email, req.Password);

        var result = await _mediator.Send(command, cancellationToken);

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