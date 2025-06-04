using Microsoft.FeatureManagement.FeatureFilters;

namespace ProductionOrderERP_API.ERP.Application.UseCase
{
    public class TenantTargetingContextAccessor : ITargetingContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantTargetingContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public ValueTask<TargetingContext> GetContextAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var tenantName = httpContext?.User.FindFirst("tenant_name")?.Value;

            Core.Helper.LogHelper.LogInformation(this.GetType().Name, "Tenant name: " + tenantName);

            var groups = new List<string>();
            groups.Add(tenantName);

            var context = new TargetingContext
            {
                Groups = groups
            };

            return ValueTask.FromResult(context);
        }
    }
}
