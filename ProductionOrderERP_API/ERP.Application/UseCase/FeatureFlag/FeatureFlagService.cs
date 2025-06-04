using Microsoft.FeatureManagement;
using ProductionOrderERP_API.ERP.Core.Entity;
using ProductionOrderERP_API.ERP.Infrastructure.Repository;

namespace ProductionOrderERP_API.ERP.Application.UseCase
{
    public class FeatureFlagService : IFeatureFlagService
    {
        private readonly IFeatureManager _featureManager;
        private readonly FeatureFlagRepository _featureFlagRepository;

        public FeatureFlagService(IFeatureManager featureManager, FeatureFlagRepository featureFlagRepository)
        {
            _featureManager = featureManager;
            _featureFlagRepository = featureFlagRepository;
        }

        public async Task<FeatureFlag> GetFeatureFlag(string featureName, int tenantId)
        {
            bool isEnabled = false;
            bool tenantFeatureMapped = await _featureFlagRepository.IsFeatureEnabledForTenantAsync(featureName, tenantId);

            if (tenantFeatureMapped)
            {
                isEnabled = await _featureManager.IsEnabledAsync(featureName);
            }

            return new FeatureFlag
            {
                Name = featureName,
                IsEnabled = isEnabled
            };
        }

        public async Task<List<FeatureFlag>> GetAllFeatureFlagsAsync(int tenantId)
        {
            return await _featureFlagRepository.GetAllFeatureFlagsAsync(tenantId);
        }
    }

}
