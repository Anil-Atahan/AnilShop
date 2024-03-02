using AnilShop.Users.Domain;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;

namespace AnilShop.Users.Endpoints.Create;

internal class Create : Endpoint<CreateUserRequest>
{
    private readonly UserManager<ApplicationUser> _userManager;
    public Create(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
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
        var newUser = new ApplicationUser
        {
            Email = req.Email,
            UserName = req.Email
        };

        await _userManager.CreateAsync(newUser, req.Password);

        await SendOkAsync();
    }
}