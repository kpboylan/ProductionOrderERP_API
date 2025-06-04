using ProductionOrderERP_API.ERP.Core.Entity;

namespace ProductionOrderERP_API.ERP.Application.UseCase
{
    public interface IFeatureFlagService
    {
        Task<List<FeatureFlag>> GetAllFeatureFlagsAsync(int tenantId);
        Task<FeatureFlag> GetFeatureFlag(string featureName, int tenantId);
    }
}