using AnilShop.SharedKernel;
using MediatR;

namespace AnilShop.Users.UseCases.GetById;

internal record GetUserByIdQuery(Guid UserId) : IRequest<Result<UserDTO>>;