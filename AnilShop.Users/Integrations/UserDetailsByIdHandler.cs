using AnilShop.SharedKernel;
using AnilShop.Users.Contracts;
using AnilShop.Users.UseCases.GetById;
using MediatR;

namespace AnilShop.Users.Integrations;

internal class UserDetailsByIdHandler :
    IRequestHandler<UserDetailsByIdQuery, Result<UserDetails>>
{
    private readonly IMediator _mediator;
    
    public UserDetailsByIdHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Result<UserDetails>> Handle(UserDetailsByIdQuery request, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(request.UserId);

        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return Result.Failure<UserDetails>(result.Error);
        }

        return new UserDetails(result.Value.UserId, result.Value.EmailAddress);
    }
}