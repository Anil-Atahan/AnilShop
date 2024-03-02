using AnilShop.SharedKernel.Abstractions;
using MediatR;

namespace AnilShop.Users.UseCases.AddCartItem;

public record AddCartItemCommand(Guid ProductId, int Quantity, string EmailAddress) : IRequest<Result>;