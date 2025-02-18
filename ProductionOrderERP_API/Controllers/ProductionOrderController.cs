
using Microsoft.AspNetCore.Mvc;
using ProductionOrderERP_API.ERP.Core.Entity;
using ProductionOrderERP_API.ERP.Core.Service;

namespace ProductionOrderERP_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductionOrderController : ControllerBase
    {
        private readonly IProdOrderService _prodOrderService;

        public ProductionOrderController(IProdOrderService prodOrderService)
        {
            _prodOrderService = prodOrderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductionOrder([FromBody] ProductionOrder productionOrder)
        {
            if (productionOrder == null)
            {
                return BadRequest("Production order data is null");
            }

            await _prodOrderService.CreateProductionOrderAsync(productionOrder);

            return CreatedAtAction(nameof(CreateProductionOrder), new { id = productionOrder.ProductionOrderID }, productionOrder);
        }
    }
}
