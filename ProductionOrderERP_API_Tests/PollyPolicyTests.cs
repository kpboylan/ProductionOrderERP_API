using AutoMapper;
using Moq;
using Polly.CircuitBreaker;
using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Application.Polly;
using ProductionOrderERP_API.ERP.Application.UseCase;
using ProductionOrderERP_API.ERP.Core.Entity;
using ProductionOrderERP_API.ERP.Core.Interface;
using ProductionOrderERP_API.ERP.Core.Service;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionOrderERP_API_Tests
{
    public class PollyPolicyTests
    {
        [Fact]
        public async Task RetryPolicy_Retries_OnBrokerUnreachableException()
        {
            // Arrange
            var callCount = 0;

            var retryPolicy = PollyPolicies.GetRabbitMqRetryPolicy();

            Func<Task> action = () =>
            {
                callCount++;
                throw new BrokerUnreachableException(new Exception("Simulated failure"));
            };

            // Act & Assert
            await Assert.ThrowsAsync<BrokerUnreachableException>(() => retryPolicy.ExecuteAsync(action));

            Assert.Equal(4, callCount); // 1 initial + 3 retries
        }

        [Fact]
        public async Task CircuitBreaker_Opens_After_Three_Exceptions()
        {
            // Arrange
            var policy = PollyPolicies.GetRabbitMqCircuitBreakerPolicy();

            Func<Task> failingAction = () =>
                throw new BrokerUnreachableException(new Exception("Simulated failure"));

            // Act
            for (int i = 0; i < 3; i++)
            {
                await Assert.ThrowsAsync<BrokerUnreachableException>(() => policy.ExecuteAsync(failingAction));
            }

            // Now circuit should be open
            var ex = await Assert.ThrowsAsync<BrokenCircuitException>(() => policy.ExecuteAsync(failingAction));
            Assert.IsType<BrokenCircuitException>(ex);
        }

        [Fact]
        public async Task FallbackPolicy_Calls_FallbackAction_OnFailure()
        {
            // Arrange
            var fallbackCalled = false;

            Func<Task> fallbackAction = () =>
            {
                fallbackCalled = true;
                return Task.CompletedTask;
            };

            var fallbackPolicy = PollyPolicies.GetRabbitMqFallbackPolicy(fallbackAction);

            Func<Task> failingAction = () =>
                throw new BrokerUnreachableException(new Exception("Simulated failure"));

            // Act
            await fallbackPolicy.ExecuteAsync(failingAction);

            // Assert
            Assert.True(fallbackCalled);
        }

        [Fact]
        public async Task WrappedPolicy_Uses_All_Policies_Correctly()
        {
            // Arrange
            var fallbackCalled = false;
            int callCount = 0;

            Func<Task> fallbackAction = () =>
            {
                fallbackCalled = true;
                return Task.CompletedTask;
            };

            var wrapped = PollyPolicies.CreateRabbitMqResiliencePolicy(fallbackAction);

            Func<Task> failingAction = () =>
            {
                callCount++;
                throw new BrokerUnreachableException(new Exception("Simulated failure"));
            };

            // Act
            await wrapped.ExecuteAsync(failingAction);

            // Assert
            Assert.True(fallbackCalled);
            Assert.Equal(4, callCount); // Retry 3 + initial
        }

        //[Fact]
        //public async Task UseCase_Falls_Back_To_Database_When_RabbitMq_Fails()
        //{
        //    // Arrange
        //    var mockRepo = new Mock<IMaterialRepository>();
        //    var mockRabbitService = new Mock<IGenericRabbitMQService<Material>>();
        //    var mockMapper = new Mock<IMapper>();
        //    var mockPolicyProvider = new RabbitMqResiliencePolicyProvider(); // or mock it too

        //    var materialEntity = new Material { MaterialID = 123 };
        //    var request = new PublishMaterialRequest { /* fields */ };
        //    var response = new PublishMaterialResponse();
        //    var pendingQueueMessage = new PendingQueueMessage();

        //    mockMapper.Setup(m => m.Map<Material>(It.IsAny<PublishMaterialRequest>())).Returns(materialEntity);
        //    mockMapper.Setup(m => m.Map<PublishMaterialResponse>(It.IsAny<Material>())).Returns(response);

        //    mockRabbitService.Setup(m => m.PublishAsync(It.IsAny<Material>(), It.IsAny<MessageBus>()))
        //                     .ThrowsAsync(new BrokerUnreachableException(new Exception("Fail")));

        //    var resiliencePolicy = mockPolicyProvider.GetResiliencePolicy(async () =>
        //    {
        //        await mockRepo.Object.CreateMaterialAsync(materialEntity);
        //    });

        //    var useCase = new PublishCreateMaterialUseCase(
        //        mockRabbitService.Object,   // IGenericRabbitMQService<Material>
        //        mockMapper.Object,          // IMapper
        //        resiliencePolicy,           // AsyncPolicyWrap
        //        mockRepo.Object,            // IMaterialRepository
        //        mockPolicyProvider,          // IRabbitMqResiliencePolicyProvider
        //        pendingQueueMessage
        //    );


        //    // Act
        //    var result = await useCase.Execute(request);

        //    // Assert
        //    mockRepo.Verify(r => r.CreateMaterialAsync(It.Is<Material>(m => m.MaterialID == 123)), Times.Once);
        //}


    }
}
