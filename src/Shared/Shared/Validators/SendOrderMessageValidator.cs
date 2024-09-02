using FluentValidation;
using Shared.Messages.Order;

namespace Shared.Validators;

public class SendOrderMessageValidator : AbstractValidator<SendOrderMessage>
{
    public SendOrderMessageValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
    }
}
