using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Application.UseCase.Room.ML;
using ProductionOrderERP_API.ERP.Infrastructure.Repository;

namespace ProductionOrderERP_API.ERP.Application.UseCase.Room
{
    public class GetRoomHumidityUseCase : ServiceBase, IGetRoomHumidityUseCase
    {
        private readonly IRoomRepository _roomRepository;
        private readonly HumiditySpikeDetection _humiditySpikeDetection;

        public GetRoomHumidityUseCase(IRoomRepository roomRepository, HumiditySpikeDetection humiditySpikeDetection)
        {
            _roomRepository = roomRepository;
            _humiditySpikeDetection = humiditySpikeDetection;
        }

        public virtual async Task<List<HumidityDtoResponse>> Execute(HumidityDtoRequest filterDto)
        {
            return await ExecuteWithHandlingAsync(async () =>
            {
                var humidList = await _roomRepository.GetRoomHumidityAsync(filterDto);
                return _humiditySpikeDetection.ProcessData(humidList, filterDto);
            }, nameof(GetRoomHumidityUseCase));
        }
    }
}
