namespace AnilShop.Users.Endpoints.ListAddresses;

internal class AddressListResponse
{
    public List<UserAddressDto> Addresses { get; set; } = new();
}