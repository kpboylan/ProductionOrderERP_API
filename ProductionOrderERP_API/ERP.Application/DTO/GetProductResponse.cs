namespace ProductionOrderERP_API.ERP.Application.DTO
{
    public class GetProductResponse
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public bool Active { get; set; } = false;
    }
}
