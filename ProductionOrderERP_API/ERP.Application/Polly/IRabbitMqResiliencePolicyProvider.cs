using Polly.Wrap;

namespace ProductionOrderERP_API.ERP.Application.Polly
{
    public interface IRabbitMqResiliencePolicyProvider
    {
        AsyncPolicyWrap GetResiliencePolicy(Func<Task> fallbackAction);
    }
}
