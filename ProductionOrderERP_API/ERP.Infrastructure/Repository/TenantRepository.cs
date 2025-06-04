using Microsoft.EntityFrameworkCore;
using ProductionOrderERP_API.ERP.Infrastructure.Persistence;

namespace ProductionOrderERP_API.ERP.Infrastructure.Repository
{
    public class TenantRepository
    {
        private readonly ERPContext _context;

        public TenantRepository(ERPContext context)
        {
            _context = context;
        }

        public async Task<string?> GetTenantNameByIdAsync(int tenantId)
        {
            return await _context.Tenants
                .Where(t => t.TenantId == tenantId)
                .Select(t => t.Name)
                .FirstOrDefaultAsync();
        }
    }
}
