using FluentValidation;

namespace AnilShop.Users.UseCases.AddCartItem;

public class AddItemToCartCommandValidator : AbstractValidator<AddItemToCartCommand>
{
    public AddItemToCartCommandValidator()
    {
        RuleFor(x => x.EmailAddress)
            .NotEmpty()
            .WithMessage("EmailAddress is required.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be at least 1.");

        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("Not a valid ProductId");
    }
}