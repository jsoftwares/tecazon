using MediatR;
using Microsoft.Extensions.Logging;

namespace Ordering.Application.Behaviors
{
    //If there is any unhandled exception, we use this class/Behavior to catch the exception into the Behaviour layer. This wasy we 
    //keep our Handler classes clean by not adding try-catch; we just centralize them here.
    //So this Behavior performs logging where there is any unhandled exception in our handler classes Handle methods
    public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : MediatR.IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public UnhandledExceptionBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            //we will like to move to d actual handle operation (try) but if there is any error, we catch this exception in here
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                /**here we get info from d class & log the RException into d PipelineBehavior. This way we don't have to add LogError
                 * or try-catch in our Handler classes for displaying error messages during an exception**/
                
                var requestName = typeof(TRequest).Name;
                _logger.LogError(ex, "Application Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);
                throw;
            }
        }
    }
}
