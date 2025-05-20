using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Core.Entity;

namespace ProductionOrderERP_API.ERP.Infrastructure.Repository
{
    public interface IRoomRepository
    {
        Task<List<HumidityDtoResponse>> GetRoomHumidityAsync(HumidityDtoRequest filterDto);
        Task<List<SensorDataDto>> GetRoomTempsAsync(SensorDataRequestDto filterDto);
    }
}