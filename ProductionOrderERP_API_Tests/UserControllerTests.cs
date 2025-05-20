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
    public class UserControllerTests
    {
        //private readonly Mock<IUserRepository> _mockUserRepository;
        //private readonly UserController _controller;
        //private readonly Mock<IMapper> _mockMapper;
        //private readonly Mock<CreateUserUseCase> _mockCreateUserUseCase;
        //private readonly Mock<GetUsersUseCase> _mockGetUsersUseCase;
        //private readonly Mock<GetUserUseCase> _mockGetUserUseCase;
        //private readonly Mock<UpdateUserUseCase> _mockUpdateUserUseCase;
        //private readonly Mock<GetUserTypesUseCase> _mockGetUserTypesUseCase;

        //public UserControllerTests()
        //{
        //    _mockMapper = new Mock<IMapper>();
        //    _mockUserRepository = new Mock<IUserRepository>();

        //    _mockCreateUserUseCase = new Mock<CreateUserUseCase>();
        //    _mockGetUsersUseCase = new Mock<GetUsersUseCase>(_mockUserRepository.Object, _mockMapper.Object);
        //    _mockGetUserUseCase = new Mock<GetUserUseCase>(_mockUserRepository.Object, _mockMapper.Object);
        //    _mockUpdateUserUseCase = new Mock<UpdateUserUseCase>();
        //    _mockGetUserTypesUseCase = new Mock<GetUserTypesUseCase>();


        //    _controller = new UserController(
        //        _mockMapper.Object,
        //        _mockCreateUserUseCase.Object,
        //        _mockGetUsersUseCase.Object,
        //        _mockGetUserUseCase.Object,
        //        _mockUpdateUserUseCase.Object,
        //        _mockGetUserTypesUseCase.Object);

        //}

        //[Fact]
        //public async Task GetUser_ReturnsOk_WithUserId()
        //{
        //    // Arrange
        //    int userId = 2;

        //    var mockUser = new GetUserResponse
        //    {
        //        UserID = 2,
        //        FirstName = "John",
        //        LastName = "Doe",
        //        Email = "jdoe@test.com",
        //        Username = "jdoe",
        //        Active = true,
        //        UserTypeID = 1
        //    };

        //    _mockGetUserUseCase
        //        .Setup(uc => uc.Execute(userId))
        //        .ReturnsAsync(mockUser);

        //    // Act
        //    var result = await _controller.GetUser(userId);

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    Assert.Equal(200, okResult.StatusCode);

        //    var returnValue = Assert.IsType<GetUserResponse>(okResult.Value);
        //    Assert.Equal(userId, returnValue.UserID);
        //    Assert.Equal("John", returnValue.FirstName);
        //    Assert.Equal("Doe", returnValue.LastName);
        //    Assert.Equal("jdoe@test.com", returnValue.Email);
        //    Assert.Equal("jdoe", returnValue.Username);
        //    Assert.True(returnValue.Active);
        //    Assert.Equal(1, returnValue.UserTypeID);
        //}

        //[Fact]
        //public async Task GetAllUsers_ReturnsOkResult()
        //{
        //    // Arrange
        //    var mockUsers = new List<GetUserResponse>
        //    {
        //        new GetUserResponse {
        //        UserID = 2,
        //        FirstName = "John",
        //        LastName = "Doe",
        //        Email = "jdoe@test.com",
        //        Username = "jdoe",
        //        Active = true,
        //        UserTypeID = 1 },

        //        new GetUserResponse {
        //        UserID = 3,
        //        FirstName = "Jane",
        //        LastName = "Smith",
        //        Email = "janesmith@test.com",
        //        Username = "janesmith",
        //        Active = true,
        //        UserTypeID = 1}
        //    };

        //    _mockGetUsersUseCase.Setup(service => service.Execute())
        //        .ReturnsAsync(mockUsers);

        //    // Act
        //    var result = await _controller.GetAllUsers();

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    Assert.Equal(200, okResult.StatusCode);

        //    var returnedUserDtos = Assert.IsAssignableFrom<List<GetUserResponse>>(okResult.Value);
        //    Assert.Equal(2, returnedUserDtos.Count);
        //}
    }
}
