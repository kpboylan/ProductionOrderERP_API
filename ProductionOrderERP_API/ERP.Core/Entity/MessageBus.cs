using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace ProductionOrderERP_API.ERP.Core.Entity
{
    public class MessageBus
    {
        public string HostName { get; set; }
        public string QueueName { get; set; }
    }
}
