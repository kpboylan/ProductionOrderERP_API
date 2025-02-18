using ProductionOrderERP_API.ERP.Core.Entity;
using ProductionOrderERP_API.ERP.Core.Helper;
using ProductionOrderERP_API.ERP.Core.Service;

namespace ProductionOrderERP_API.ERP.Application.Service
{
    public class ProdOrderService : IProdOrderService
    {
        public Task<ProductionOrder> CreateProductionOrderAsync(ProductionOrder productionOrder)
        {
            ValidateProdOrder.ProductExists();
            //productionOrder.Id = _products.Max(p => p.Id) + 1;  // Simple ID assignment
            //_products.Add(product);
            return Task.FromResult(productionOrder);
        }
    }
}
