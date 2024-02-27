using AnilShop.SharedKernel.Abstractions;
using MediatR;

namespace AnilShop.Products.Contracts;

public record ProductDetailsQuery(Guid ProductId) : IRequest<Result<ProductDetailsResponse>>;