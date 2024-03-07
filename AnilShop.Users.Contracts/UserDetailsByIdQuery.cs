using AnilShop.SharedKernel;
using MediatR;

namespace AnilShop.Users.Contracts;

public record UserDetailsByIdQuery(Guid UserId) :
    IRequest<Result<UserDetails>>;