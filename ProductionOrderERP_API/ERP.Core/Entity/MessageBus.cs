using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace ProductionOrderERP_API.ERP.Core.Entity
{
    public class MessageBus
    {
        public string HostName { get; set; }
        public string QueueName { get; set; }

        //public async Task<string> Publish(T item, MessageBus messageBus)
        //{
        //    var factory = new ConnectionFactory { HostName = messageBus.HostName };

        //    using var connection = await factory.CreateConnectionAsync();

        //    using var channel = await connection.CreateChannelAsync();

        //    await channel.QueueDeclareAsync(queue: messageBus.QueueName,
        //                                    durable: false,
        //                                    exclusive: false,
        //                                    autoDelete: false,
        //                                    arguments: null);

        //    var json = JsonConvert.SerializeObject(item);
        //    var body = Encoding.UTF8.GetBytes(json);

        //    var properties = new BasicProperties();
        //    properties.Persistent = false;

        //    await channel.BasicPublishAsync(exchange: string.Empty,
        //                        routingKey: messageBus.QueueName,
        //                        mandatory: false,
        //                        basicProperties: properties,
        //                        body: new ReadOnlyMemory<byte>(body));

        //    return $"Processed item of type {typeof(T).Name}: {item}";
        //}
    }
}
