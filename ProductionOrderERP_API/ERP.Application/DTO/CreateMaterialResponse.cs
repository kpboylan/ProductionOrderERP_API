namespace ProductionOrderERP_API.ERP.Application.DTO
{
    public class CreateMaterialResponse
    {
        public int MaterialID { get; set; }
        public int MaterialType { get; set; }
        public string? MaterialName { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; }
        public string? UOMCode { get; set; }
        public string? MaterialTypeAbbreviation { get; set; }
        public int UOMId { get; set; }
        public decimal CurrentStock { get; set; } = 0;
    }
}
