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
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly CreateUserUseCase _createUserUseCase;
        private readonly GetUsersUseCase _getUsersUseCase;
        private readonly GetUserUseCase _getUserUseCase;
        private readonly UpdateUserUseCase _updateUserUseCase;
        private readonly ValidateUserUseCase _validateUserUseCase;
        private readonly GetUserTypesUseCase _getUserTypesUseCase;
        public UserController(IUserRepository userRepository, 
            IUserService userService, 
            IMapper mapper,
            CreateUserUseCase createUserUseCase,
            GetUsersUseCase getUsersUseCase,
            GetUserUseCase getUserUseCase,
            UpdateUserUseCase updateUserUseCase,
            ValidateUserUseCase validateUserUseCase,
            GetUserTypesUseCase getUserTypesUseCase)
        {
            _userRepository = userRepository;
            _userService = userService;
            _mapper = mapper;
            _createUserUseCase = createUserUseCase;
            _getUserUseCase = getUserUseCase;
            _getUsersUseCase = getUsersUseCase;
            _updateUserUseCase = updateUserUseCase;
            _validateUserUseCase = validateUserUseCase;
            _getUserTypesUseCase = getUserTypesUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest user)
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

            await _updateUserUseCase.Execute(userId, user);

            return NoContent();
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
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
