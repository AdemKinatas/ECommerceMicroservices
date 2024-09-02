using FluentValidation;

namespace Basket.Application.Features.Basket.Commands.DeleteBasket;

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId is required");
    }
}
