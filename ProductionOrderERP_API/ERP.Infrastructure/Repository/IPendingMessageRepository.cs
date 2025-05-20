using ProductionOrderERP_API.ERP.Core.Entity;

namespace ProductionOrderERP_API.ERP.Infrastructure.Repository
{
    public interface IPendingMessageRepository
    {
        Task<PendingQueueMessage> CreatePendingMessageAsync(PendingQueueMessage message);
    }
}