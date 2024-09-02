using FluentValidation;
using Shared.Messages.Basket;

namespace Shared.Validators;

public class AddBasketMessageValidator : AbstractValidator<AddBasketMessage>
{
    public AddBasketMessageValidator()
    {
        RuleFor(x => x.ProductName).NotEmpty().WithMessage("Ürün adı boş olamaz.");
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Miktar 0'dan büyük olmalıdır.");
    }
}
