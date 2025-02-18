namespace ProductionOrderERP_API.ERP.Core.Entity
{
    public class ProductionOrderRawMaterial
    {
        public int ProductionOrderRawMaterialID { get; set; }  // Corresponds to INT PRIMARY KEY IDENTITY(1,1)
        public int ProductionOrderID { get; set; }             // Foreign key to ProductionOrder
        public int RawMaterialID { get; set; }                 // Foreign key to RawMaterial
        public decimal QuantityUsed { get; set; }              // Corresponds to DECIMAL(18, 2)

        // Navigation properties (optional, for use with an ORM like Entity Framework)
        public ProductionOrder ProductionOrder { get; set; }   // Navigation property to ProductionOrder
        public RawMaterial RawMaterial { get; set; }           // Navigation property to RawMaterial
    }
}
