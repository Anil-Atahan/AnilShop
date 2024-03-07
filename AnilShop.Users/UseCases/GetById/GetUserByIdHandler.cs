using AnilShop.SharedKernel;
using AnilShop.Users.Interfaces;
using MediatR;

namespace AnilShop.Users.UseCases.GetById;

internal class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, Result<UserDTO>>
{
    private readonly IApplicationUserRepository _applicationUserRepository;

    public GetUserByIdHandler(IApplicationUserRepository applicationUserRepository)
    {
        _applicationUserRepository = applicationUserRepository;
    }

    public async Task<Result<UserDTO>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _applicationUserRepository.GetUserByIdAsync(request.UserId);

        if (user is null)
        {
            return Result.Failure<UserDTO>(Error.None);
        }

        return new UserDTO(Guid.Parse(user!.Id), user.Email!);
    }
}