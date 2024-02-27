using AnilShop.SharedKernel.Abstractions;
using AnilShop.Users.CartEndpoints.ListItems;
using AnilShop.Users.Errors;
using MediatR;

namespace AnilShop.Users.UseCases;

internal class ListCartItemsQueryHandler : IRequestHandler<ListCartItemsQuery, Result<List<CartItemDto>>>
{
    private readonly IApplicationUserRepository _applicationUserRepository;

    public ListCartItemsQueryHandler(IApplicationUserRepository applicationUserRepository)
    {
        _applicationUserRepository = applicationUserRepository;
    }

    public async Task<Result<List<CartItemDto>>> Handle(ListCartItemsQuery request, CancellationToken cancellationToken)
    {
        var user = await _applicationUserRepository.GetUserWithCartByEmailAsync(request.EmailAddress);
        if (user is null)
        {
            return Result.Failure<List<CartItemDto>>(UserErrors.NotFound);
        }

        return user.CartItems
            .Select(item => new CartItemDto(item.Id, item.ProductId,
                item.Description, item.Quantity, item.UnitPrice))
            .ToList();
    }
}