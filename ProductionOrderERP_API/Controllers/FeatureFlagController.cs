using Microsoft.AspNetCore.Mvc;
using ProductionOrderERP_API.ERP.Application.UseCase;

namespace ProductionOrderERP_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeatureFlagController : ControllerBase
    {
        private readonly IFeatureFlagService _featureFlagService;

        public FeatureFlagController(IFeatureFlagService featureFlagService)
        {
            _featureFlagService = featureFlagService;
        }

        [HttpGet("{featureName}")]
        public async Task<IActionResult> GetFeatureFlag(string featureName)
        {
            var tenantIdClaim = HttpContext.User.FindFirst("tenant_id")?.Value;

            if (!int.TryParse(tenantIdClaim, out int tenantId))
            {
                return Unauthorized("Invalid tenant.");
            }

            var flag = await _featureFlagService.GetFeatureFlag(featureName, tenantId);

            return Ok(flag);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllFeatureFlags()
        {
            var tenantIdClaim = HttpContext.User.FindFirst("tenant_id")?.Value;

            if (!int.TryParse(tenantIdClaim, out int tenantId))
            {
                return Unauthorized("Invalid tenant.");
            }

            var flags = await _featureFlagService.GetAllFeatureFlagsAsync(tenantId);

            return Ok(flags);
        }
    }
}
