using AnilShop.SharedKernel;
using MediatR;

namespace AnilShop.Users.UseCases.AddAddress;

internal record AddAddressToUserCommand(
    string EmailAddress,
    string Street1,
    string Street2,
    string City,
    string State,
    string PostalCode,
    string Country) : IRequest<Result>;