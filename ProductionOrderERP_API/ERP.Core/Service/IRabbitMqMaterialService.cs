

using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Core.Entity;

namespace ProductionOrderERP_API.ERP.Core.Service
{
    public interface IRabbitMqMaterialService
    {
        Task<Material> CreateMaterialAsync(Material material);
        Task<Material> UpdateMaterialAsync(int materialId, Material material);
    }
}