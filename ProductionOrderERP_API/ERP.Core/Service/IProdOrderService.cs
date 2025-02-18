
using ProductionOrderERP_API.ERP.Core.Entity;

namespace ProductionOrderERP_API.ERP.Core.Service
{
    public interface IProdOrderService
    {
        Task<ProductionOrder> CreateProductionOrderAsync(ProductionOrder productionOrder);
    }
}
