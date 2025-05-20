using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Application.UseCase.Room;
using ProductionOrderERP_API.ERP.Application.UseCase.Room.ML;
using ProductionOrderERP_API.ERP.Core.Interface;
using ProductionOrderERP_API.ERP.Infrastructure.Repository;

namespace ProductionOrderERP_API.ERP.Application.UseCase
{
    public class GetRoomTempUseCase : ServiceBase, IGetRoomTempUseCase
    {
        private readonly IRoomRepository _roomRepository;
        private readonly TempAnomalyDetection _tempAnomalyDetection;

        public GetRoomTempUseCase(IRoomRepository roomRepository, TempAnomalyDetection tempAnomalyDetection)
        {
            _roomRepository = roomRepository;
            _tempAnomalyDetection = tempAnomalyDetection;
        }

        public virtual async Task<List<SensorDataDto>> Execute(SensorDataRequestDto filterDto)
        {
            return await ExecuteWithHandlingAsync(async () =>
            {
                var temps = await _roomRepository.GetRoomTempsAsync(filterDto);
                return _tempAnomalyDetection.ProcessData(temps, filterDto);
            }, nameof(GetRoomTempUseCase));
        }
    }
}
