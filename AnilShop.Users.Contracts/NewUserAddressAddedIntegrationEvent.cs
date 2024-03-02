namespace AnilShop.Users.Contracts;

public record NewUserAddressAddedIntegrationEvent(UserAddressDetails Details)
    : IntegrationEventBase;