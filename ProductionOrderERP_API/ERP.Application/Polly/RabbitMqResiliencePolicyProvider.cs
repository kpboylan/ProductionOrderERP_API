using Polly.Wrap;
using Polly;
using RabbitMQ.Client.Exceptions;
using System;
using ProductionOrderERP_API.ERP.Core.Entity;

namespace ProductionOrderERP_API.ERP.Application.Polly
{
    public class RabbitMqResiliencePolicyProvider : IRabbitMqResiliencePolicyProvider
    {
        public AsyncPolicyWrap GetResiliencePolicy(Func<Task> fallbackAction)
        {
            //var retryPolicy = Policy
            //    .Handle<BrokerUnreachableException>()
            //    .Or<AlreadyClosedException>()
            //    .Or<TimeoutException>()
            //    .Or<IOException>()
            //    .WaitAndRetryAsync(
            //        retryCount: 3,
            //        sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
            //        onRetry: (ex, ts, retryCount, ctx) =>
            //            Core.Helper.LogHelper.LogServiceError(nameof(PollyPolicies), $"Retry {retryCount} due to {ex.Message}")
            //    );

            var retryPolicy = Policy
                .Handle<BrokerUnreachableException>()
                .Or<AlreadyClosedException>()
                .Or<TimeoutException>()
                .Or<IOException>()
                .WaitAndRetryAsync(
                    retryCount: 3,
                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry: (ex, ts, retryCount, ctx) =>
                    {
                        Core.Helper.LogHelper.LogServiceError(
                            nameof(PollyPolicies),
                            $"Retry {retryCount} due to {ex.Message}"
                        );

                        // Optional: update RetryCount in the PendingQueueMessage
                        if (ctx.ContainsKey("PendingMessage") && ctx["PendingMessage"] is PendingQueueMessage msg)
                        {
                            msg.RetryCount = retryCount;
                            msg.LastAttempt = DateTime.UtcNow;
                        }
                    });

            var circuitBreakerPolicy = Policy
                .Handle<BrokerUnreachableException>()
                .Or<TimeoutException>()
                .CircuitBreakerAsync(
                    exceptionsAllowedBeforeBreaking: 3,
                    durationOfBreak: TimeSpan.FromSeconds(10),
                    onBreak: (ex, delay) =>
                        Core.Helper.LogHelper.LogServiceError(nameof(PollyPolicies), $"Circuit broken! No retries for {delay.TotalSeconds}s."),
                    onReset: () =>
                        Core.Helper.LogHelper.LogInformation(nameof(PollyPolicies), "Circuit reset! Retries enabled.")
                );

            var fallbackPolicy = Policy
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
                    onFallbackAsync: async (ex) =>
                    {
                        Core.Helper.LogHelper.LogServiceError(nameof(PollyPolicies), $"[Fallback] Triggered because: {ex.Message}");
                        await Task.CompletedTask;
                    });

            return Policy.WrapAsync(fallbackPolicy, circuitBreakerPolicy, retryPolicy);
        }
    }
}
