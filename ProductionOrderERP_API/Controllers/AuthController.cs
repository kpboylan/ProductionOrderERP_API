using Microsoft.AspNetCore.Mvc;
using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Application.UseCase;
using ProductionOrderERP_API.ERP.Core.Entity;
using Serilog;

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
            try
            {
                var userTask = _validateUserUseCase.Execute(loginRequest);

                if (userTask == null)
                {
                    Log.Error("Invalid username or password.");
                    return Unauthorized("Invalid username or password.");
                }

                User user = await userTask;

                if (user == null)
                {
                    Log.Error("Invalid username or password.");
                    return Unauthorized("Invalid username or password.");
                }

                var token = await _generateTokenUseCase.Execute(user);

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                ERP.Core.Helper.LogHelper.LogControllerError(ControllerContext.ActionDescriptor.ControllerName, ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
