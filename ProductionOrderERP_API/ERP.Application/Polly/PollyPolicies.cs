using Polly.Retry;
using Polly;
using RabbitMQ.Client.Exceptions;
using Polly.CircuitBreaker;
using Polly.Fallback;
using Polly.Wrap;

namespace ProductionOrderERP_API.ERP.Application.Polly
{
    public static class PollyPolicies
    {
        public static AsyncRetryPolicy GetRabbitMqRetryPolicy()
        {
            return Policy
                .Handle<BrokerUnreachableException>()
                .Or<AlreadyClosedException>()
                .Or<TimeoutException>()
                .Or<System.IO.IOException>()
                .WaitAndRetryAsync(
                    retryCount: 3,
                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry: (exception, timeSpan, retryCount, context) =>
                    {
                        Core.Helper.LogHelper.LogServiceError(nameof(PollyPolicies), $"Retry {retryCount} due to {exception.Message}");
                    });
        }

        public static AsyncCircuitBreakerPolicy GetRabbitMqCircuitBreakerPolicy()
        {
            return Policy
                .Handle<BrokerUnreachableException>()
                .Or<TimeoutException>()
                .CircuitBreakerAsync(
                exceptionsAllowedBeforeBreaking: 3,
                durationOfBreak: TimeSpan.FromSeconds(30),
                onBreak: (exception, breakDelay) =>
                    Core.Helper.LogHelper.LogServiceError(nameof(PollyPolicies), $"Circuit broken! No retries for {breakDelay.TotalSeconds}s."),
                onReset: () => Core.Helper.LogHelper.LogInformation(nameof(PollyPolicies), "Circuit reset! Retries enabled."));
        }

        public static AsyncFallbackPolicy GetRabbitMqFallbackPolicy(Func<Task> fallbackAction)
        {
            return Policy
                .Handle<BrokerUnreachableException>()
                .Or<AlreadyClosedException>()
                .Or<TimeoutException>()
                .Or<IOException>()
                .FallbackAsync(
                    fallbackAction: async (ct) =>
                    {
                        Core.Helper.LogHelper.LogInformation(nameof(PollyPolicies), "[Fallback] Executing fallback due to RabbitMQ failure.");
                        await fallbackAction();
                    },
                    onFallbackAsync: async (exception) =>
                    {
                        Core.Helper.LogHelper.LogServiceError(nameof(PollyPolicies), $"[Fallback] Triggered because: {exception.Message}");
                        await Task.CompletedTask;
                    });
        }

        public static AsyncPolicyWrap CreateRabbitMqResiliencePolicy(Func<Task> fallbackAction)
        {
            var retryPolicy = GetRabbitMqRetryPolicy();
            var circuitBreakerPolicy = GetRabbitMqCircuitBreakerPolicy();
            var fallbackPolicy = GetRabbitMqFallbackPolicy(fallbackAction);

            return Policy.WrapAsync(fallbackPolicy, circuitBreakerPolicy, retryPolicy);
        }
    }
}
