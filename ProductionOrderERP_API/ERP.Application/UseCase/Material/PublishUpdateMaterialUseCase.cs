using AutoMapper;
using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Application.Enums;
using ProductionOrderERP_API.ERP.Core.Entity;
using ProductionOrderERP_API.ERP.Core.Service;

namespace ProductionOrderERP_API.ERP.Application.UseCase
{
    public class PublishUpdateMaterialUseCase
    {
        private readonly IGenericRabbitMQService<Material> _genericRabbitMQService;
        private readonly IMapper _mapper;
        private readonly MessageBus _bus;
        private readonly PendingQueueMessage _pendingQueueMessage;

        public PublishUpdateMaterialUseCase(IGenericRabbitMQService<Material> genericRabbitMQService,
    IMapper mapper,
    PendingQueueMessage pendingQueueMessage)
        {
            _genericRabbitMQService = genericRabbitMQService;
            _mapper = mapper;
            _bus = new MessageBus();
            _bus.HostName = MessageQueue.MessageHostName.LocalHost.ToString();
            _bus.QueueName = MessageQueue.MessageQueueName.UpdateMaterialQueue.ToString();
            _pendingQueueMessage = pendingQueueMessage;
        }

        public async Task<PublishMaterialResponse> Execute(PublishMaterialRequest request, int materialId)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            try
            {
                var materialEntity = _mapper.Map<Material>(request);

                await _genericRabbitMQService.PublishAsync(materialEntity, _bus, _pendingQueueMessage);

                var response = _mapper.Map<PublishMaterialResponse>(materialEntity);

                return response;
            }
            catch (Exception ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);

                throw new ApplicationException("An error occurred while processing your request.", ex);
            }
        }
    }
}
