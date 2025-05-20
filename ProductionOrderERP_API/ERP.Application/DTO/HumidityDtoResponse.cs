namespace ProductionOrderERP_API.ERP.Application.DTO
{
    public class HumidityDtoResponse
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public double Humidity { get; set; }
        public bool IsSpike { get; set; }
        public double Score { get; set; }
        public double PValue { get; set; }
        public string RoomName { get; set; } = null!;
        public string BatchNumber { get; set; } = null!;
    }
}
