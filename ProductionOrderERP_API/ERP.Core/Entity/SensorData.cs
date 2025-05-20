namespace ProductionOrderERP_API.ERP.Core.Entity
{
    public class SensorData
    {
        public int Id { get; set; }

        public int RoomId { get; set; }

        public int BatchId { get; set; }
        public DateTime Timestamp { get; set; }
        public double Temperature { get; set; }
    }
}
