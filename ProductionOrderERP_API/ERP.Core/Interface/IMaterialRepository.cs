

using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Core.Entity;

namespace ProductionOrderERP_API.ERP.Core.Interface
{
    public interface IMaterialRepository
    {
        Task<Material> CreateMaterialAsync(Material material);
        Task<List<GetMaterialResponse>> GetActiveMaterialsNewAsync();
        Task<Material?> GetMaterialAsync(int materialId);
        Task<List<Material>> GetMaterialsAsync();
        Task<List<GetMaterialResponse>> GetMaterialsNewAsync();
        Task<List<MaterialType>> GetMaterialTypesAsync();
        Task<List<UOM>> GetUOMAsync();
        Task<Material> UpdateMaterialAsync(Material material);
    }
}