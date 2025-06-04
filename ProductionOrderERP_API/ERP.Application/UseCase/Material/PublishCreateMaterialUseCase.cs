using AutoMapper;
using Polly;
using Polly.Wrap;
using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Application.Enums;
using ProductionOrderERP_API.ERP.Application.Polly;
using ProductionOrderERP_API.ERP.Core.Entity;
using ProductionOrderERP_API.ERP.Core.Interface;
using ProductionOrderERP_API.ERP.Core.Service;
using ProductionOrderERP_API.ERP.Infrastructure.Repository;

namespace ProductionOrderERP_API.ERP.Application.UseCase
{
    public class PublishCreateMaterialUseCase
    {
        private readonly IGenericRabbitMQService<Material> _genericRabbitMQService;
        private readonly IMapper _mapper;
        private readonly MessageBus _bus;
        private readonly IRabbitMqResiliencePolicyProvider _policyProvider;
        private readonly PendingQueueMessage _pendingQueueMessage;
        public readonly IPendingMessageRepository _pendingMessageRepository;

        public PublishCreateMaterialUseCase(IGenericRabbitMQService<Material> genericRabbitMQService,
            IMapper mapper, AsyncPolicyWrap asyncPolicyWrap, IMaterialRepository materialRepository, 
            IRabbitMqResiliencePolicyProvider policyProvider, PendingQueueMessage pendingQueueMessage,
            IPendingMessageRepository pendingMessageRepository)
        {
            _genericRabbitMQService = genericRabbitMQService;
            _mapper = mapper;
            _bus = new MessageBus();
            _bus.HostName = MessageQueue.MessageHostName.LocalHost.ToString();
            //_bus.HostName = MessageQueue.DockerMessageHostName.rabbitmq.ToString();
            _bus.QueueName = MessageQueue.MessageQueueName.AddMaterialQueue.ToString();
            _policyProvider = policyProvider;
            _pendingQueueMessage = pendingQueueMessage;
            _pendingMessageRepository = pendingMessageRepository;
        }

        public async Task<PublishMaterialResponse> Execute(PublishMaterialRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }


            try
            {
                var materialEntity = _mapper.Map<Material>(request);

                _pendingQueueMessage.EntityType = "Material";
                _pendingQueueMessage.QueueName = _bus.QueueName;

                var fallbackAction = () => _pendingMessageRepository.CreatePendingMessageAsync(_pendingQueueMessage);
                var policy = _policyProvider.GetResiliencePolicy(fallbackAction);

                var context = new Context();
                context["PendingMessage"] = _pendingQueueMessage;

                await policy.ExecuteAsync(async (ctx) =>
                {
                    await _genericRabbitMQService.PublishAsync(materialEntity, _bus, _pendingQueueMessage);
                }, context);

                var response = _mapper.Map<PublishMaterialResponse>(materialEntity);

                return response;
            }
            catch (Exception ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);

                throw;
            }
        }
    }
}
