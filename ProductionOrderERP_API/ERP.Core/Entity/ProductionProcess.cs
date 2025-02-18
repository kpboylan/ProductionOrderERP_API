namespace ProductionOrderERP_API.ERP.Core.Entity
{
    public class ProductionProcess
    {
        public int ProductionProcessID { get; set; }   // Corresponds to INT PRIMARY KEY IDENTITY(1,1)
        public string ProcessName { get; set; }        // Corresponds to NVARCHAR(100)
        public string Description { get; set; }        // Corresponds to NVARCHAR(255)
        public decimal EstimatedTimeInHours { get; set; } // Corresponds to DECIMAL(5, 2)
        public string Status { get; set; }             // Corresponds to NVARCHAR(50)
    }
}
