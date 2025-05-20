using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ProductionOrderERP_API.ERP.Infrastructure.Service
{
    public interface IServiceBusSender
    {
        Task SendMessageAsync<T>(T messageObject, string queueName);
    }
    public class ServiceBusSender : IServiceBusSender, IAsyncDisposable
    {
        private readonly ServiceBusClient _client;
        private bool _disposed;

        public ServiceBusSender(string serviceBusConnectionString)
        {
            _client = new ServiceBusClient(serviceBusConnectionString);
        }

        public async Task SendMessageAsync<T>(T messageObject, string queueName)
        {
            // Create sender
            var sender = _client.CreateSender(queueName);

            try
            {
                string messageBody = JsonConvert.SerializeObject(messageObject);
                var message = new ServiceBusMessage(messageBody);

                await sender.SendMessageAsync(message);
            }
            finally
            {
                await sender.DisposeAsync();
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (!_disposed)
            {
                await _client.DisposeAsync();
                _disposed = true;
            }
        }
    }
}
