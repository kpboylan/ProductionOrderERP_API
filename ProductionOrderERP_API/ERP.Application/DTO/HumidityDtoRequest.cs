namespace ProductionOrderERP_API.ERP.Application.DTO
{
    public class HumidityDtoRequest
    {
        public int RoomId { get; set; }

        public int BatchId { get; set; }

        public bool? IsSpike { get; set; } = false;
    }
}
