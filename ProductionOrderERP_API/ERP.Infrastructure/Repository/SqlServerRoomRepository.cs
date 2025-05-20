using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Application.UseCase;
using ProductionOrderERP_API.ERP.Core.Entity;
using ProductionOrderERP_API.ERP.Infrastructure.Persistence;

namespace ProductionOrderERP_API.ERP.Infrastructure.Repository
{
    public class SqlServerRoomRepository : ServiceBase, IRoomRepository
    {
        private readonly ERPContext _context;

        public SqlServerRoomRepository(ERPContext context)
        {
            _context = context;
        }

        public async Task<List<SensorDataDto>> GetRoomTempsAsync(SensorDataRequestDto filterDto)
        {
            try
            {
                return await (from reading in _context.SensorReadings
                              join batch in _context.Batches on reading.BatchId equals batch.Id
                              join room in _context.FactoryRooms on reading.RoomId equals room.Id
                              where reading.BatchId == filterDto.BatchId && reading.RoomId == filterDto.RoomId
                              select new SensorDataDto
                              {
                                  Id = reading.Id,
                                  Timestamp = reading.Timestamp,
                                  Temperature = reading.Temperature,
                                  BatchNumber = batch.BatchNumber,
                                  RoomName = room.RoomName
                              }).ToListAsync();

                //return await _context.SensorReadings
                //    .Where(sr => sr.BatchId == filterDto.BatchId && sr.RoomId == filterDto.RoomId)
                //    .Select(sr => new SensorDataDto
                //    {
                //        Id = sr.Id,
                //        Timestamp = sr.Timestamp,
                //        Temperature = sr.Temperature
                //    })
                //    .ToListAsync();
            }
            catch (SqlException ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);
                throw;
            }
        }

        public async Task<List<HumidityDtoResponse>> GetRoomHumidityAsync(HumidityDtoRequest filterDto)
        {
            try
            {
                 return await (from reading in _context.HumidityReadings
                              join batch in _context.Batches on reading.BatchId equals batch.Id
                              join room in _context.FactoryRooms on reading.RoomId equals room.Id
                              where reading.BatchId == filterDto.BatchId && reading.RoomId == filterDto.RoomId
                              select new HumidityDtoResponse
                              {
                                  Id = reading.Id,
                                  Timestamp = reading.Timestamp,
                                  Humidity = reading.Humidity,
                                  BatchNumber = batch.BatchNumber,
                                  RoomName = room.RoomName
                              }).ToListAsync();
            }
            catch (SqlException ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);
                throw;
            }
        }
    }
}
