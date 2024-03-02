using AnilShop.Users.Domain;

namespace AnilShop.Users.Interfaces;

internal interface IApplicationUserRepository
{
    Task<ApplicationUser> GetUserWithCartByEmailAsync(string email);
    Task SaveChangesAsync();
    Task<ApplicationUser> GetUserWithAddressesByEmailAsync(string email);
}