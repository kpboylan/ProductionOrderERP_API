using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Hosting;
using ProductionOrderERP_API.ERP.Core.Entity;
using ProductionOrderERP_API.ERP.Core.Interface;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ProductionOrderERP_API.ERP.Infrastructure.Service
{
    public class MaterialQueueProcessor : BackgroundService
    {
        private readonly ServiceBusProcessor _processor;
        private readonly IMaterialRepository _materialRepository;
        private const string QueueName = "material-creation-queue";

        public MaterialQueueProcessor(
            string serviceBusConnectionString,
            IMaterialRepository materialRepository)
        {
            var client = new ServiceBusClient(serviceBusConnectionString);
            _processor = client.CreateProcessor(QueueName, new ServiceBusProcessorOptions());
            _materialRepository = materialRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _processor.ProcessMessageAsync += ProcessMessageHandler;
            _processor.ProcessErrorAsync += ErrorHandler;

            await _processor.StartProcessingAsync(stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }

            await _processor.StopProcessingAsync(stoppingToken);
        }

        private async Task ProcessMessageHandler(ProcessMessageEventArgs args)
        {
            try
            {
                string messageBody = args.Message.Body.ToString();
                var material = JsonSerializer.Deserialize<Material>(messageBody);

                // Process the material (insert into DB)
                await _materialRepository.CreateMaterialAsync(material);

                // Complete the message so it's removed from the queue
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception ex)
            {
                // Log error or handle dead-letter queue
                await args.AbandonMessageAsync(args.Message);
            }
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            // Log the exception
            Console.WriteLine($"Message handler encountered an exception: {args.Exception}.");
            return Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            await _processor.DisposeAsync();
            await base.StopAsync(stoppingToken);
        }
    }
}
