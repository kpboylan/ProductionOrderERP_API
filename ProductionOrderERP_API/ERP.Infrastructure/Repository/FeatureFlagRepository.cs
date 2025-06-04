using Microsoft.EntityFrameworkCore;
using ProductionOrderERP_API.ERP.Core.Entity;
using ProductionOrderERP_API.ERP.Infrastructure.Persistence;

namespace ProductionOrderERP_API.ERP.Infrastructure.Repository
{
    public class FeatureFlagRepository
    {
        private readonly ERPContext _context;

        public FeatureFlagRepository(ERPContext context)
        {
            _context = context;
        }

        public async Task<bool> IsFeatureEnabledForTenantAsync(string featureName, int tenantId)
        {
            return await _context.FeatureFlags
                .Where(f => f.Name == featureName && f.IsEnabled)
                .SelectMany(f => f.FeatureFlagTenants)
                .AnyAsync(ft => ft.TenantId == tenantId);
        }

        public async Task<List<FeatureFlag>> GetAllFeatureFlagsAsync(int tenantId)
        {
            return await _context.FeatureFlagTenants
                .Where(ft => ft.TenantId == tenantId)
                .Select(ft => ft.FeatureFlag)
                .ToListAsync();
        }
    }
}
