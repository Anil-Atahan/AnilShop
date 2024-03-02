namespace AnilShop.Users.Domain;

internal class UserStreetAddress
{
    public UserStreetAddress(string userId, Address streetAddress)
    {
        UserId = userId;
        StreetAddress = streetAddress;
    }

    // EF
    private UserStreetAddress()
    {
        
    }

    public Guid Id { get; private set; } = Guid.NewGuid();
    public string UserId { get; private set; } = string.Empty;
    public Address StreetAddress { get; private set; } = default!;
}