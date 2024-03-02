using AnilShop.SharedKernel.Abstractions;
using AnilShop.Users.Endpoints.ListAddresses;
using MediatR;

namespace AnilShop.Users.UseCases.ListAddresses;

internal record ListAddressesQuery(string EmailAddress) : IRequest<Result<List<UserAddressDto>>>;