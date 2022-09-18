using System;
using FluentValidation;
using MediatR;
using ValidationException = Ordering.Application.Exceptions.ValidationException;
using System.Threading;
using System.Threading.Tasks;
namespace Ordering.Application.Behaviors
{
    /** We add application Behavior using Mediator through the IPipelineBehavior interface it gives us to add Behavior before (Pre) 
     * or after (Post) Handle Request. In our case, we will add 2 Behaviors; 1 for the validation behavior: in other to run the 
     * validations and if there is any error, we can throw the exception. And also an Unhandled exception behavour: if there is 
     * exception, we can throw the exception, then log the information with the Pipeline behavior features.
     * Behavior intercepts request before or after they proceed to the handler class. More like middlewares 
     **/
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : MediatR.IRequest<TResponse>
    {
        /**We require all the Validators which come from  the  IValidator object from the FluentValidation package. ie we collect all
         * d IValidator objects from d assembly using d reflection of d Ivalidator; in our case we have 2 validator object: one for
         * d CheckoutOrderCommandValidator & UpdateOrderCommandValidator **/
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            //is there any validator in my assembly using d FluentValidator (we have 2) then perform d validate operations
            //ValidationContext comes from FluentValidator
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                //runs d validator for every IValidator classes (2 in our case) & returns all d results
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                //checks if there is any validation failure when validation if performed. IF there is any error
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count > 0)
                    //by default ValidationException uses FluentValidation & this will not work as expected in our case, so above we
                    //specify a using statement to point to our custom ValidationException with USING statement
                    throw new ValidationException(failures);
            }

            //we need to call next() in other to continue our request pipeline or request handle method in d mediator
            return await next();
        }

    }
}
