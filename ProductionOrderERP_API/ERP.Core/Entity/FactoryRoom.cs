namespace ProductionOrderERP_API.ERP.Core.Entity
{
    public class FactoryRoom
    {
        public int Id { get; set; }
        public string RoomID { get; set; } = null!;
        public string RoomName { get; set; } = null!;
        public string? Description { get; set; }
    }
}
