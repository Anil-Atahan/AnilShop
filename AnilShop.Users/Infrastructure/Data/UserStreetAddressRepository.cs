using AnilShop.Users.Domain;
using AnilShop.Users.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AnilShop.Users.Infrastructure.Data;

internal class UserStreetAddressRepository : IReadOnlyUserStreetAddressRepository
{
    private readonly UsersDbContext _usersDbContext;

    public UserStreetAddressRepository(UsersDbContext usersDbContext)
    {
        _usersDbContext = usersDbContext;
    }

    public Task<UserStreetAddress?> GetByIdAsync(Guid userStreetAddressId)
    {
        return _usersDbContext.UserStreetAddresses
            .SingleOrDefaultAsync(a => a.Id == userStreetAddressId);
    }
}