using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Application.UseCase;
using ProductionOrderERP_API.ERP.Core.Entity;
using ProductionOrderERP_API.ERP.Core.Interface;
using ProductionOrderERP_API.ERP.Core.Service;

namespace ProductionOrderERP_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CreateUserUseCase _createUserUseCase;
        private readonly GetUsersUseCase _getUsersUseCase;
        private readonly GetActiveUsersUseCase _getActiveUsersUseCase;
        private readonly GetUserUseCase _getUserUseCase;
        private readonly UpdateUserUseCase _updateUserUseCase;
        private readonly GetUserTypesUseCase _getUserTypesUseCase;
        public UserController(
            IMapper mapper,
            CreateUserUseCase createUserUseCase,
            GetUsersUseCase getUsersUseCase,
            GetActiveUsersUseCase getActiveUsersUseCase,
            GetUserUseCase getUserUseCase,
            UpdateUserUseCase updateUserUseCase,
            GetUserTypesUseCase getUserTypesUseCase)
        {
            _mapper = mapper;
            _createUserUseCase = createUserUseCase;
            _getUserUseCase = getUserUseCase;
            _getUsersUseCase = getUsersUseCase;
            _getActiveUsersUseCase = getActiveUsersUseCase;
            _updateUserUseCase = updateUserUseCase;
            _getUserTypesUseCase = getUserTypesUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("User data is required.");
                }

                var userEntity = _mapper.Map<User>(user);
                var createdUser = await _createUserUseCase.Execute(userEntity);

                if (createdUser == null)
                {
                    return StatusCode(500, "An error occurred while creating the user.");
                }

                return CreatedAtAction(nameof(CreateUser), new { id = createdUser.UserID }, createdUser);
            }
            catch (Exception ex)
            {
                ERP.Core.Helper.LogHelper.LogControllerError(ControllerContext.ActionDescriptor.ControllerName, ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var userDtos = await _getUsersUseCase.Execute();

                return Ok(userDtos);
            }
            catch (Exception ex)
            {
                ERP.Core.Helper.LogHelper.LogControllerError(ControllerContext.ActionDescriptor.ControllerName, ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveUsers()
        {
            try
            {
                var userDtos = await _getActiveUsersUseCase.Execute();

                return Ok(userDtos);
            }
            catch (Exception ex)
            {
                ERP.Core.Helper.LogHelper.LogControllerError(ControllerContext.ActionDescriptor.ControllerName, ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("getUser")]
        public async Task<IActionResult> GetUser(int userId)
        {
            try
            {
                var userDto = await _getUserUseCase.Execute(userId);

                return Ok(userDto);
            }
            catch (Exception ex)
            {
                ERP.Core.Helper.LogHelper.LogControllerError(ControllerContext.ActionDescriptor.ControllerName, ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser([FromBody] GetUserRequest user, int userId)
        {
            if (user == null)
            {
                return BadRequest("User data is null");
            }

            try
            {
                await _updateUserUseCase.Execute(userId, user);

                return NoContent();
            }
            catch (Exception ex)
            {
                ERP.Core.Helper.LogHelper.LogControllerError(ControllerContext.ActionDescriptor.ControllerName, ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("userTypes")]
        public async Task<IActionResult> GetUserTypes()
        {
            try
            {
                var userTypeDtos = await _getUserTypesUseCase.Execute();

                return Ok(userTypeDtos);
            }
            catch (Exception ex)
            {
                ERP.Core.Helper.LogHelper.LogControllerError(ControllerContext.ActionDescriptor.ControllerName, ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
