namespace ProductionOrderERP_API.ERP.Core.Entity
{
    public class ProductionBatch
    {
        public int ProductionBatchID { get; set; }      // Corresponds to INT PRIMARY KEY IDENTITY(1,1)
        public int ProductionOrderID { get; set; }      // Foreign key to ProductionOrder
        public string BatchNumber { get; set; }         // Corresponds to NVARCHAR(50)
        public DateTime? BatchStartDate { get; set; }   // Corresponds to DATETIME (nullable)
        public DateTime? BatchEndDate { get; set; }     // Corresponds to DATETIME (nullable)
        public string Status { get; set; }              // Corresponds to NVARCHAR(50)
        public decimal? QuantityProduced { get; set; }  // Corresponds to DECIMAL(18, 2), nullable for cases like zero or not yet produced

        // Navigation property (optional, for use with an ORM like Entity Framework)
        public ProductionOrder ProductionOrder { get; set; } // Navigation property to ProductionOrder
    }
}
