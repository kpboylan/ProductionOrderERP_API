

using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Core.Entity;

namespace ProductionOrderERP_API.ERP.Core.Service
{
    public interface IMaterialService
    {
        Task<Material> CreateMaterialAsync(Material material);
        Task<List<GetMaterialResponse>> GetMaterialsAsync();
        Task<List<GetMaterialTypeResponse>> GetMaterialTypesAsync();
        Task<List<GetUOMRequest>> GetUOMsAsync();
        Task<GetMaterialResponse> GetMaterialAsync(int materialId);
        Task<Material?> UpdateMaterialAsync(int materialId, GetMaterialRequest material);
    }
}