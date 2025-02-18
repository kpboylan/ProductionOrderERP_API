using ProductionOrderERP_API.ERP.Core.Entity;

namespace ProductionOrderERP_API.ERP.Core.Service
{
    public interface IGenericRabbitMQService<in T> where T : class
    {
        Task<string> PublishAsync(T item, MessageBus messageBus);
    }
}
