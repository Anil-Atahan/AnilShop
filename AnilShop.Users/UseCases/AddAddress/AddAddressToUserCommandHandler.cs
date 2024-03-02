using AnilShop.SharedKernel.Abstractions;
using MediatR;
using Serilog;

namespace AnilShop.Users.UseCases.AddAddress;

internal class AddAddressToUserCommandHandler : IRequestHandler<AddAddressToUserCommand, Result>
{
    private readonly IApplicationUserRepository _userRepository;
    private readonly ILogger _logger;

    public AddAddressToUserCommandHandler(IApplicationUserRepository userRepository, ILogger logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<Result> Handle(AddAddressToUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserWithAddressesByEmailAsync(request.EmailAddress);

        if (user is null)
        {
            return Result.Failure(Error.None);
        }

        var addressToAdd = new Address(
            request.Street1,
            request.Street2,
            request.City,
            request.State,
            request.PostalCode,
            request.Country);

        var userAddress = user.AddAddress(addressToAdd);
        await _userRepository.SaveChangesAsync();
        
        _logger.Information(
            "[UseCase] Added address {address} to user {email} (Total addresses {addressCount})",
            userAddress.StreetAddress,
            request.EmailAddress,
            user.Addresses.Count);

        return Result.Success();
    }
}