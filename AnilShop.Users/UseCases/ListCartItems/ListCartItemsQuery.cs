using AnilShop.SharedKernel;
using AnilShop.Users.CartEndpoints.ListItems;
using MediatR;

namespace AnilShop.Users.UseCases.ListCartItems;

public record ListCartItemsQuery(string EmailAddress) : IRequest<Result<List<CartItemDto>>>;