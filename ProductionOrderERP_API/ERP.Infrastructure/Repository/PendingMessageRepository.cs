using ProductionOrderERP_API.ERP.Core.Entity;
using ProductionOrderERP_API.ERP.Infrastructure.Persistence;

namespace ProductionOrderERP_API.ERP.Infrastructure.Repository
{
    public class PendingMessageRepository : IPendingMessageRepository
    {
        private readonly ERPContext _context;

        public PendingMessageRepository(ERPContext context)
        {
            _context = context;
        }

        public async Task<PendingQueueMessage> CreatePendingMessageAsync(PendingQueueMessage message)
        {
            _context.PendingQueueMessages.Add(message);
            await _context.SaveChangesAsync();

            return await Task.FromResult(message);
        }
    }
}
