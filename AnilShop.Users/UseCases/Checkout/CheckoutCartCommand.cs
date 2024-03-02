using AnilShop.SharedKernel;
using MediatR;

namespace AnilShop.Users.UseCases.Checkout;

internal record CheckoutCartCommand(string EmailAddress,
    Guid ShippingAddressId,
    Guid BillingAddressId) : IRequest<Result<Guid>>;