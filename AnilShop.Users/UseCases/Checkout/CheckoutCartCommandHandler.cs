using AnilShop.OrderProcessing.Contracts;
using AnilShop.SharedKernel;
using AnilShop.Users.Interfaces;
using MediatR;

namespace AnilShop.Users.UseCases.Checkout;

internal class CheckoutCartCommandHandler : IRequestHandler<CheckoutCartCommand, Result<Guid>>
{
    private readonly IMediator _mediator;
    private readonly IApplicationUserRepository _userRepository;

    public CheckoutCartCommandHandler(IMediator mediator, IApplicationUserRepository userRepository)
    {
        _mediator = mediator;
        _userRepository = userRepository;
    }

    public async Task<Result<Guid>> Handle(CheckoutCartCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserWithCartByEmailAsync(request.EmailAddress);

        if (user is null)
        {
            return Result.Failure<Guid>(Error.None);
        }

        var items = user.CartItems.Select(item =>
            new OrderItemDetails(item.ProductId,
                item.Title,
                item.Quantity,
                item.UnitPrice,
                item.Description)).ToList();

        var createOrderCommand = new CreateOrderCommand(Guid.Parse(user.Id),
            request.ShippingAddressId,
            request.BillingAddressId,
            items);

        var result = await _mediator.Send(createOrderCommand);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(Error.None);
        }

        user.ClearCart();
        await _userRepository.SaveChangesAsync();

        return Result.Success(result.Value.OrderId);
    }
}