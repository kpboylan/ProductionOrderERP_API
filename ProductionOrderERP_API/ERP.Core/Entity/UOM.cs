namespace ProductionOrderERP_API.ERP.Core.Entity
{
    public class UOM
    {
        public int UOMID { get; set; }
        public string UOMCode { get; set; }
        public string UOMDescription { get; set; }

        public string UOMType { get; set; }
        public bool IsActive { get; set; }
    }
}
