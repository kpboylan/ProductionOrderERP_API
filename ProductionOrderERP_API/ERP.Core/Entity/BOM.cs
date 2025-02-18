namespace ProductionOrderERP_API.ERP.Core.Entity
{
    public class BOM
    {
        public int BomID { get; set; }               // Unique identifier for each BOM entry
        public string ItemNumber { get; set; }        // Item number or code
        public string Description { get; set; }       // Description of the material or equipment
        public string PartNumber { get; set; }        // Part number for the item
        public int Quantity { get; set; }             // Quantity of the item
        public string UnitOfMeasure { get; set; }     // Unit of measure (e.g., mL, Each, etc.)
        public string Supplier { get; set; }          // Supplier name
        public decimal CostPerUnit { get; set; }      // Cost per unit
        public decimal TotalCost                      // Computed column for total cost
        {
            get { return Quantity * CostPerUnit; }    // Computed value: Quantity * CostPerUnit
        }
        public string Notes { get; set; }             // Additional notes (e.g., special storage or handling)

        // Constructor to initialize the BOM object
        public BOM(int bomId, string itemNumber, string description, string partNumber,
                   int quantity, string unitOfMeasure, string supplier,
                   decimal costPerUnit, string notes)
        {
            BomID = bomId;
            ItemNumber = itemNumber;
            Description = description;
            PartNumber = partNumber;
            Quantity = quantity;
            UnitOfMeasure = unitOfMeasure;
            Supplier = supplier;
            CostPerUnit = costPerUnit;
            Notes = notes;
        }
    }
}
