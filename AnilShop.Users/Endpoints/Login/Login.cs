using AnilShop.Users.Domain;
using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Identity;

namespace AnilShop.Users.Endpoints.Login;

internal class Login : Endpoint<UserLoginRequest>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public Login(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public override void Configure()
    {
        Post("/users/login");
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(
        UserLoginRequest req,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(req.Email);
        if (user is null)
        {
            await SendUnauthorizedAsync();
            return;
        }

        var loginSuccessful = await _userManager.CheckPasswordAsync(user, req.Password);

        if (!loginSuccessful)
        {
            await SendUnauthorizedAsync();
            return;
        }

        var jwtSecret = Config["Auth:JwtSecret"]!;
        var token = JWTBearer.CreateToken(jwtSecret,
            p => p["EmailAddress"] = user.Email!);
    }
}