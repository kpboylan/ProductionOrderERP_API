

using ProductionOrderERP_API.ERP.Core.Entity;

namespace ProductionOrderERP_API.ERP.Core.Service
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(User user);
        User ValidateToken(string token);
    }
}