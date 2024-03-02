using AnilShop.Users.Domain;

namespace AnilShop.Users.Interfaces;

internal interface IReadOnlyUserStreetAddressRepository
{
    Task<UserStreetAddress?> GetByIdAsync(Guid userStreetAddressId);
}