namespace ProductionOrderERP_API.ERP.Core.Entity
{
    public class ProductionOrder
    {
        public int ProductionOrderID { get; set; }  // Corresponds to INT PRIMARY KEY IDENTITY(1,1)
        public string OrderNumber { get; set; }     // Corresponds to NVARCHAR(50)
        public int? ProductID { get; set; }        // Corresponds to INT (nullable foreign key)
        public DateTime OrderDate { get; set; }     // Corresponds to DATETIME
        public DateTime? DueDate { get; set; }      // Corresponds to DATETIME (nullable)
        public string Status { get; set; }          // Corresponds to NVARCHAR(50)
        public decimal QuantityRequested { get; set; } // Corresponds to DECIMAL(18, 2)
        public decimal QuantityProduced { get; set; } // Corresponds to DECIMAL(18, 2) with default value 0
        public int? ProductionBatchID { get; set; }  // Corresponds to INT (nullable foreign key)
        public DateTime? CreatedDate { get; set; }   // Corresponds to DATETIME (nullable)
        public DateTime? UpdatedDate { get; set; }   // Corresponds to DATETIME (nullable)
    }
}
