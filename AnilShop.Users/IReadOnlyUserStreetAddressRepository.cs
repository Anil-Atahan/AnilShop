namespace AnilShop.Users;

internal interface IReadOnlyUserStreetAddressRepository
{
    Task<UserStreetAddress?> GetByIdAsync(Guid userStreetAddressId);
}