namespace ProductionOrderERP_API.ERP.Core.Entity
{
    public class PendingQueueMessage
    {
        public int Id { get; set; }

        // E.g., "Material", "Product", etc.
        public string EntityType { get; set; }

        // JSON-serialized domain object
        public string Payload { get; set; }

        public string QueueName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int RetryCount { get; set; } = 0;

        public DateTime? LastAttempt { get; set; }

        public bool IsPublished { get; set; } = false;
    }
}
