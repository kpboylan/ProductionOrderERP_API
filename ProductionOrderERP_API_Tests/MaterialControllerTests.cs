using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductionOrderERP_API.Controllers;
using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Application.UseCase;
using ProductionOrderERP_API.ERP.Core.Interface;
using ProductionOrderERP_API.ERP.Core.Service;
using ProductionOrderERP_API.ERP.Infrastructure.Persistence;

namespace ProductionOrderERP_API_Tests
{
    public class MaterialControllerTests
    {
        private readonly Mock<GetMaterialsUseCase> _mockGetMaterialsUseCase;
        private readonly Mock<IRabbitMqMaterialService> _mockRabbitMqMaterialService;
        private readonly Mock<IMaterialRepository> _mockMaterialRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ERPContext _context;
        private readonly MaterialController _controller;
        private readonly Mock<CreateMaterialUseCase> _mockCreateMaterialUseCase;
        private readonly Mock<GetMaterialUseCase> _mockGetMaterialUseCase;
        private readonly Mock<GetActiveMaterialsUseCase> _mockGetActiveMaterialsUseCase;
        private readonly Mock<GetUOMsUseCase> _mockGetUOMsUseCase;
        private readonly Mock<UpdateMaterialUseCase> _mockUpdateMaterialUseCase;
        private readonly Mock<GetMaterialTypesUseCase> _mockGetMaterialTypesUseCase;
        private readonly Mock<PublishCreateMaterialUseCase> _mockPublishCreateMaterialUseCase;
        private readonly Mock<PublishUpdateMaterialUseCase> _mockPublishUpdateMaterialUseCase;

        public MaterialControllerTests()
        {
            _mockMaterialRepository = new Mock<IMaterialRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockRabbitMqMaterialService = new Mock<IRabbitMqMaterialService>();
            _mockCreateMaterialUseCase = new Mock<CreateMaterialUseCase>();
            _mockGetMaterialUseCase = new Mock<GetMaterialUseCase>();
            _mockGetActiveMaterialsUseCase = new Mock<GetActiveMaterialsUseCase>();
            _mockGetUOMsUseCase = new Mock<GetUOMsUseCase>();
            _mockUpdateMaterialUseCase = new Mock<UpdateMaterialUseCase>();
            _mockGetMaterialTypesUseCase = new Mock<GetMaterialTypesUseCase>();
            _mockPublishCreateMaterialUseCase = new Mock<PublishCreateMaterialUseCase>();
            _mockPublishUpdateMaterialUseCase = new Mock<PublishUpdateMaterialUseCase>();

            _controller = new MaterialController(
                _mockCreateMaterialUseCase.Object,
                _mockGetUOMsUseCase.Object,
                _mockGetMaterialsUseCase.Object,
                _mockGetActiveMaterialsUseCase.Object,
                _mockGetMaterialUseCase.Object,
                _mockUpdateMaterialUseCase.Object,
                _mockGetMaterialTypesUseCase.Object,
                _mockPublishCreateMaterialUseCase.Object,
                _mockPublishUpdateMaterialUseCase.Object

            );
        }

        [Fact]
        public async Task GetMaterials_ReturnsOkResult_WhenMaterialsAreAvailable()
        {
            // Arrange
            var mockMaterialDtos = new List<GetMaterialResponse>
            {
                new GetMaterialResponse {
                    MaterialID = 1,
                    MaterialType = 1,
                    MaterialName = "Lactose",
                    Description = "A sugar used as an excipient in tablets and capsules",
                    CurrentStock = 5000 },

                new GetMaterialResponse {
                    MaterialID = 2,
                    MaterialType = 2,
                    MaterialName = "Cellulose",
                    Description = "Used as a binder and filler in tablets and capsules",
                    CurrentStock = 10000 }
            };

            _mockGetMaterialsUseCase.Setup(service => service.Execute())
                .ReturnsAsync(mockMaterialDtos);

            // Act
            var result = await _controller.GetMaterials();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedMaterialDtos = Assert.IsAssignableFrom<List<GetMaterialResponse>>(okResult.Value);
            Assert.Equal(2, returnedMaterialDtos.Count);
        }
    }
}
