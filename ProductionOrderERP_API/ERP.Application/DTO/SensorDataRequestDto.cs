namespace ProductionOrderERP_API.ERP.Application.DTO
{
    public class SensorDataRequestDto
    {
        public int RoomId { get; set; }

        public int BatchId { get; set; }

        public bool? IsAnomaly { get; set; } = false;
    }
}
