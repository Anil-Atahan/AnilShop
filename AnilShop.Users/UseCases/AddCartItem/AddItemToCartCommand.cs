using AnilShop.SharedKernel;
using MediatR;

namespace AnilShop.Users.UseCases.AddCartItem;

public record AddItemToCartCommand(Guid ProductId, int Quantity, string EmailAddress) : IRequest<Result>;