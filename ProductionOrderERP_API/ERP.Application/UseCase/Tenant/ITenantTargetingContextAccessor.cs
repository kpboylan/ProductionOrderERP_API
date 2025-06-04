using Microsoft.FeatureManagement.FeatureFilters;

namespace ProductionOrderERP_API.ERP.Application.UseCase
{
    public interface ITenantTargetingContextAccessor
    {
        ValueTask<TargetingContext> GetContextAsync();
    }
}