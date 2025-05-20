using AutoMapper;
using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Core.Entity;

namespace ProductionOrderERP_API.ERP.Application.Mapping
{
    public class RoomProfile : Profile
    {
        public RoomProfile() 
        {
            CreateMap<SensorData, SensorDataDto>();
            CreateMap<SensorDataDto, SensorData>();

            CreateMap<HumidityReading, HumidityDtoResponse>();
            CreateMap<HumidityDtoResponse, HumidityReading>();
        }
    }
}
