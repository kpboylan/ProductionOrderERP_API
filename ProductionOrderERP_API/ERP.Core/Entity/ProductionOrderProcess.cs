namespace ProductionOrderERP_API.ERP.Core.Entity
{
    public class ProductionOrderProcess
    {
        public int ProductionOrderProcessID { get; set; } // Corresponds to INT PRIMARY KEY IDENTITY(1,1)
        public int ProductionOrderID { get; set; }        // Foreign key to ProductionOrder
        public int ProductionProcessID { get; set; }      // Foreign key to ProductionProcess
        public DateTime? StartDate { get; set; }          // Corresponds to DATETIME (nullable)
        public DateTime? EndDate { get; set; }            // Corresponds to DATETIME (nullable)
        public string Status { get; set; }                // Corresponds to NVARCHAR(50)

        // Navigation properties (optional, for use with an ORM like Entity Framework)
        public ProductionOrder ProductionOrder { get; set; } // Navigation property to ProductionOrder
        public ProductionProcess ProductionProcess { get; set; } // Navigation property to ProductionProcess
    }
}
