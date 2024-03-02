using AnilShop.SharedKernel.Abstractions;
using MediatR;

namespace AnilShop.Users.Contracts;

public record UserAddressDetailsByIdQuery(Guid AddressId) :
    IRequest<Result<UserAddressDetails>>;
