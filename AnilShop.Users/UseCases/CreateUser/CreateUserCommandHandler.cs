using AnilShop.SharedKernel;
using MediatR;

namespace AnilShop.Users.UseCases.CreateUser;

internal record CreateUserCommand(string Email, string Password) : IRequest<Result>;