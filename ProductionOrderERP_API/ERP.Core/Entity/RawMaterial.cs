namespace ProductionOrderERP_API.ERP.Core.Entity
{
    public class RawMaterial
    {
        public int RawMaterialID { get; set; }    // Corresponds to INT PRIMARY KEY IDENTITY(1,1)
        public string MaterialName { get; set; }   // Corresponds to NVARCHAR(100)
        public string Description { get; set; }    // Corresponds to NVARCHAR(255)
        public string UnitOfMeasure { get; set; }  // Corresponds to NVARCHAR(20)
        public decimal CurrentStock { get; set; }  // Corresponds to DECIMAL(18, 2)
    }
}
