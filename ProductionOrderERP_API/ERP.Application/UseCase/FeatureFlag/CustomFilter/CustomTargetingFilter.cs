using Microsoft.FeatureManagement.FeatureFilters;
using Microsoft.FeatureManagement;

namespace ProductionOrderERP_API.ERP.Application.UseCase
{
    public class CustomTargetingFilter : IFeatureFilter
    {
        private readonly TargetingFilter _inner;

        public CustomTargetingFilter(ITargetingContextAccessor targetingContextAccessor)
        {
            _inner = new TargetingFilter(targetingContextAccessor);
        }

        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            return _inner.EvaluateAsync(context);
        }
    }
}
