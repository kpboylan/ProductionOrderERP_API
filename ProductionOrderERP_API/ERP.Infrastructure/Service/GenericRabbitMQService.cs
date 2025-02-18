using Newtonsoft.Json;
using ProductionOrderERP_API.ERP.Core.Entity;
using ProductionOrderERP_API.ERP.Core.Service;
using RabbitMQ.Client;
using static ProductionOrderERP_API.ERP.Application.Enums.MessageQueue;
using System.Text;

namespace ProductionOrderERP_API.ERP.Infrastructure.Service
{
    public class GenericRabbitMQService<T> : IGenericRabbitMQService<T> where T : class
    {
        public async Task<string> PublishAsync(T item, MessageBus messageBus)
        {
            var factory = new ConnectionFactory { HostName = messageBus.HostName };

            using var connection = await factory.CreateConnectionAsync();

            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: messageBus.QueueName,
                                            durable: false,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);

            var json = JsonConvert.SerializeObject(item);
            var body = Encoding.UTF8.GetBytes(json);

            var properties = new BasicProperties();
            properties.Persistent = false;

            await channel.BasicPublishAsync(exchange: string.Empty,
                                routingKey: messageBus.QueueName,
                                mandatory: false,
                                basicProperties: properties,
                                body: new ReadOnlyMemory<byte>(body));

            return $"Processed item of type {typeof(T).Name}: {item}";
        }
    }
}
