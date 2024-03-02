using AnilShop.Users.Domain;
using AnilShop.Users.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AnilShop.Users.Infrastructure.Data;

internal class ApplicationUserRepository : IApplicationUserRepository
{
    private readonly UsersDbContext _usersDbContext;

    public ApplicationUserRepository(UsersDbContext usersDbContext)
    {
        _usersDbContext = usersDbContext;
    }

    public Task<ApplicationUser> GetUserWithCartByEmailAsync(string email)
    {
        return _usersDbContext.ApplicationUsers
            .Include(user => user.CartItems)
            .SingleAsync(user => user.Email == email);
    }

    public Task SaveChangesAsync()
    {
        return _usersDbContext.SaveChangesAsync();
    }

    public Task<ApplicationUser> GetUserWithAddressesByEmailAsync(string email)
    {
        return _usersDbContext.ApplicationUsers
            .Include(user => user.Addresses)
            .SingleAsync(user => user.Email == email);
    }
}