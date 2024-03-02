using AnilShop.SharedKernel;
using MediatR;

namespace AnilShop.Products.Contracts;

public record ProductDetailsQuery(Guid ProductId) : IRequest<Result<ProductDetailsResponse>>;