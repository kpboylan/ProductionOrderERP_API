namespace ProductionOrderERP_API.ERP.Core.Entity
{
    public class HumidityReading
    {
        public int Id { get; set; }

        public int RoomId { get; set; }

        public int BatchId { get; set; }
        public DateTime Timestamp { get; set; }
        public double Humidity { get; set; }
    }
}
