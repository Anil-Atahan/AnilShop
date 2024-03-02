using AnilShop.SharedKernel.Abstractions;
using AnilShop.Users.Contracts;
using MediatR;

namespace AnilShop.Users.Integrations;

internal class UserAddressDetailsByIdQueryHandler :
    IRequestHandler<UserAddressDetailsByIdQuery, Result<UserAddressDetails>>
{
    private readonly IReadOnlyUserStreetAddressRepository _addressRepository;

    public UserAddressDetailsByIdQueryHandler(IReadOnlyUserStreetAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task<Result<UserAddressDetails>> Handle(UserAddressDetailsByIdQuery request, CancellationToken cancellationToken)
    {
        var address = await _addressRepository.GetByIdAsync(request.AddressId);

        if (address is null) return Result.Failure<UserAddressDetails>(Error.None);

        Guid userId = Guid.Parse(address.UserId);

        var details = new UserAddressDetails(userId,
            address.Id,
            address.StreetAddress.Street1,
            address.StreetAddress.Street2,
            address.StreetAddress.City,
            address.StreetAddress.State,
            address.StreetAddress.PostalCode,
            address.StreetAddress.Country);

        return details;
    }
}