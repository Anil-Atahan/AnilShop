namespace AnilShop.Users;

internal interface IApplicationUserRepository
{
    Task<ApplicationUser> GetUserWithCartByEmailAsync(string email);
    Task SaveChangesAsync();
    Task<ApplicationUser> GetUserWithAddressesByEmailAsync(string email);
}