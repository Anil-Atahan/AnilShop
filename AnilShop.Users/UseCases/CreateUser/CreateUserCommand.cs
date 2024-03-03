using AnilShop.EmailSending.Conracts;
using AnilShop.SharedKernel;
using AnilShop.Users.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AnilShop.Users.UseCases.CreateUser;

internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMediator _mediator;

    public CreateUserCommandHandler(UserManager<ApplicationUser> userManager,
        IMediator mediator)
    {
        _userManager = userManager;
        _mediator = mediator;
    }

    public async Task<Result> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var newUser = new ApplicationUser
        {
            Email = command.Email,
            UserName = command.Email
        };

        var result = await _userManager.CreateAsync(newUser, command.Password);

        if (!result.Succeeded)
        {
            var error = new Error("Identity.Error", string.Join(',',result.Errors.Select(e => e.Description).ToArray()));
            return Result.Failure(error);
        }

        // send welcome email
        var sendEmailCommand = new SendEmailCommand
        {
            To = command.Email,
            From = "donotreply@test.com",
            Subject = "Welcome to AnilShop!",
            Body = "Thank you for registering."
        };

        _ = await _mediator.Send(sendEmailCommand);

        return Result.Success();
    }
}