using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    /**We use this to add a validator to CheckoutOrder which will be a Pre Processor Bhevavior that should be invoked
     * by the mediator before the request handler processes d request.
     * This class inherits AbstractValidator class which is from the FluentValidation package **/
    public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator()
        {
            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("{Username} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{Username} must not exceed 50 characters");

            RuleFor(p => p.EmailAddress)
                .NotEmpty().WithMessage("{EmailAddress} is required");

            RuleFor(p => p.TotalPrice)
                .NotEmpty().WithMessage("{TotalPrice} is required")
                .GreaterThan(0).WithMessage("{TotalPrice} must not greater than 0");
        }
    }
}
