namespace ProductionOrderERP_API.ERP.Core.Entity
{
    public class Batch
    {
        public int Id { get; set; }
        public string BatchNumber { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; } = null!;
    }
}
