using Microsoft.EntityFrameworkCore;

namespace AnilShop.Users.Data;

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
}