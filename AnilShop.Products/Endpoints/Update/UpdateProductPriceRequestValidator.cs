using FastEndpoints;
using FluentValidation;

namespace AnilShop.Products.Endpoints.Update;

public class UpdateProductPriceRequestValidator : Validator<UpdateProductPriceRequest>
{
    public UpdateProductPriceRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEqual(Guid.Empty)
            .WithMessage("A product id is required.");

        RuleFor(x => x.NewPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Product prices may not be negative.");
    }
}