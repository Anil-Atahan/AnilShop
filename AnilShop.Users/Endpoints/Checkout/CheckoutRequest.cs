namespace AnilShop.Users.Endpoints.Checkout;

internal record CheckoutRequest(
    Guid ShippingAddressId,
    Guid BillingAddressId);