using ProductionOrderERP_API.ERP.Application.DTO;

namespace ProductionOrderERP_API.ERP.Application.UseCase.Room
{
    public interface IGetRoomHumidityUseCase
    {
        Task<List<HumidityDtoResponse>> Execute(HumidityDtoRequest filterDto);
    }
}