using AnilShop.Products.Contracts;
using AnilShop.SharedKernel;
using AnilShop.Users.Domain;
using AnilShop.Users.Errors;
using AnilShop.Users.Interfaces;
using MediatR;

namespace AnilShop.Users.UseCases.AddCartItem;

internal class AddItemToCartHandler : IRequestHandler<AddItemToCartCommand, Result>
{
    private readonly IApplicationUserRepository _applicationUserRepository;
    private readonly IMediator _mediator;

    public AddItemToCartHandler(IApplicationUserRepository applicationUserRepository, IMediator mediator)
    {
        _applicationUserRepository = applicationUserRepository;
        _mediator = mediator;
    }
    
    public async Task<Result> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
    {
        var user = await _applicationUserRepository.GetUserWithCartByEmailAsync(request.EmailAddress);
        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound);
        }

        var query = new ProductDetailsQuery(request.ProductId);

        var result = await _mediator.Send(query);

        if (result.IsFailure) return Result.Failure(result.Error);

        var productDetails = result.Value;

        var newCartItem = new CartItem(request.ProductId,
            productDetails.Title,
            productDetails.Description,
            request.Quantity,
            productDetails.Price);

        user.AddItemToCart(newCartItem);
        
        await _applicationUserRepository.SaveChangesAsync();
        return Result.Success();
    }
}