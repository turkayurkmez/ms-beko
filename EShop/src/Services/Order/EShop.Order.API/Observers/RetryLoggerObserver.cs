using MassTransit;

namespace EShop.Order.API.Observers
{
    public class RetryLoggerObserver(ILogger<RetryLoggerObserver> logger)  : IRetryObserver
    {
        Task IRetryObserver.PostCreate<T>(RetryPolicyContext<T> context)
        {
            logger.LogInformation($"RetryPolicyContext: {context}");
            return Task.CompletedTask;
        }

        Task IRetryObserver.PostFault<T>(RetryContext<T> context)
        {
            logger.LogInformation($"RetryContext: {context}");
            return Task.CompletedTask;


        }

        Task IRetryObserver.PreRetry<T>(RetryContext<T> context)
        {
            logger.LogInformation($"RetryContext: {context}");
            return Task.CompletedTask;

        }

        Task IRetryObserver.RetryComplete<T>(RetryContext<T> context)
        {
            throw new NotImplementedException();
        }

        Task IRetryObserver.RetryFault<T>(RetryContext<T> context)
        {
            throw new NotImplementedException();
        }
    }
}
