using Microsoft.AspNetCore.Mvc;
using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Application.UseCase;
using ProductionOrderERP_API.ERP.Application.UseCase.Room;

namespace ProductionOrderERP_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IGetRoomTempUseCase _getRoomTempUseCase;
        private readonly IGetRoomHumidityUseCase _getRoomHumidityUseCase;

        public RoomController(IGetRoomTempUseCase getRoomTempUseCase, IGetRoomHumidityUseCase getRoomHumidityUseCase)
        {
            _getRoomTempUseCase = getRoomTempUseCase;
            _getRoomHumidityUseCase = getRoomHumidityUseCase;
        }

        [HttpGet("roomTempList")]
        public async Task<IActionResult> GetRoomTempList([FromQuery] SensorDataRequestDto filterDto)
        {
            try
            {
                var tempList = await _getRoomTempUseCase.Execute(filterDto);

                return Ok(tempList);
            }
            catch (Exception ex)
            {
                ERP.Core.Helper.LogHelper.LogControllerError(ControllerContext.ActionDescriptor.ControllerName, ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("roomHumidityList")]
        public async Task<IActionResult> GetRoomHumidityList([FromQuery] HumidityDtoRequest filterDto)
        {
            try
            {
                var humidityList = await _getRoomHumidityUseCase.Execute(filterDto);

                return Ok(humidityList);
            }
            catch (Exception ex)
            {
                ERP.Core.Helper.LogHelper.LogControllerError(ControllerContext.ActionDescriptor.ControllerName, ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
