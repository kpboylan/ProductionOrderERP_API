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

        public PublishUpdateMaterialUseCase(IGenericRabbitMQService<Material> genericRabbitMQService,
    IMapper mapper)
        {
            _genericRabbitMQService = genericRabbitMQService;
            _mapper = mapper;
            _bus = new MessageBus();
            _bus.HostName = MessageQueue.MessageHostName.LocalHost.ToString();
            _bus.QueueName = MessageQueue.MessageQueueName.UpdateMaterial.ToString();
        }

        public async Task<PublishMaterialResponse> Execute(PublishMaterialRequest request, int materialId)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var materialEntity = _mapper.Map<Material>(request);

            await _genericRabbitMQService.PublishAsync(materialEntity, _bus);

            var response = _mapper.Map<PublishMaterialResponse>(materialEntity);

            return response;
        }
    }
}
