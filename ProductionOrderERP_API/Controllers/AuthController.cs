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
    public class AuthController : ControllerBase
    {
        private readonly ValidateUserUseCase _validateUserUseCase;
        private readonly GenerateTokenUseCase _generateTokenUseCase;

        public AuthController(
            ValidateUserUseCase validateUserUseCase,
            GenerateTokenUseCase generateTokenUseCase)
        {
            _validateUserUseCase = validateUserUseCase;
            _generateTokenUseCase = generateTokenUseCase;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var userTask = _validateUserUseCase.Execute(loginRequest);

            if (userTask == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            User user = await userTask;

            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            var token = _generateTokenUseCase.Execute(user);

            return Ok(new { Token = token });
        }

    }
}
