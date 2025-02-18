namespace ProductionOrderERP_API.ERP.Core.Entity
{
    public class QualityControl
    {
        public int QualityControlID { get; set; }       // Corresponds to INT PRIMARY KEY IDENTITY(1,1)
        public int ProductionOrderID { get; set; }      // Foreign key to ProductionOrder
        public DateTime QCDate { get; set; }            // Corresponds to DATETIME
        public string TestName { get; set; }            // Corresponds to NVARCHAR(100)
        public string TestResult { get; set; }          // Corresponds to NVARCHAR(50)
        public decimal? ResultValue { get; set; }       // Corresponds to DECIMAL(18, 2), nullable for cases like non-numeric results
        public string Status { get; set; }              // Corresponds to NVARCHAR(50)

        // Navigation property (optional, for use with an ORM like Entity Framework)
        public ProductionOrder ProductionOrder { get; set; } // Navigation property to ProductionOrder
    }
}
