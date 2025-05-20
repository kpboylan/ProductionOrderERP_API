using ProductionOrderERP_API.ERP.Application.DTO;

namespace ProductionOrderERP_API.ERP.Application.UseCase
{
    public interface IGetRoomTempUseCase
    {
        //Task<List<SensorDataDto>> Execute(int batchId, int roomId);

        Task<List<SensorDataDto>> Execute(SensorDataRequestDto filterDto);
    }
}