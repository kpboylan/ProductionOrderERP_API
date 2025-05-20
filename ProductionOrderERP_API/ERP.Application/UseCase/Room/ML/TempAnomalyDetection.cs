using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.TimeSeries;
using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Core.Entity;

namespace ProductionOrderERP_API.ERP.Application.UseCase.Room.ML
{
    public class TempAnomalyDetection
    {
        public List<SensorDataDto> ProcessData(List<SensorDataDto> dataList, SensorDataRequestDto filterDto)
        {
            List<SensorDataDto> dtoList = new List<SensorDataDto>();

            var mlContext = new MLContext();

            var mlInputList = dataList
                .Select(x => new TempInput { Temperature = (float)x.Temperature })
                .ToList();

            var dataView = mlContext.Data.LoadFromEnumerable(mlInputList);


            // Define anomaly detection pipeline
            var pipeline = mlContext.Transforms.DetectAnomalyBySrCnn(
            outputColumnName: nameof(SensorPrediction.Prediction),
            inputColumnName: nameof(SensorData.Temperature),
            windowSize: 24,
            backAddWindowSize: 5,
            lookaheadWindowSize: 5,
            averagingWindowSize: 5,
            judgementWindowSize: 8,
            threshold: 0.35
            );

            // Train model
            var model = pipeline.Fit(dataView);

            // Apply to entire dataset
            var transformed = pipeline.Fit(dataView).Transform(dataView);
            var predictions = mlContext.Data
                .CreateEnumerable<SensorPrediction>(transformed, reuseRowObject: false)
                .ToList();

            if (predictions.Count != dataList.Count)
            {
                throw new Exception($"Prediction count mismatch: predictions={predictions.Count}, data={dataList.Count}");
            }

            for (int i = 0; i < dataList.Count; i++)
            {
                var dto = new SensorDataDto();
                var reading = dataList[i];
                var prediction = predictions[i].Prediction;

                dto.Temperature = reading.Temperature;
                dto.Timestamp = reading.Timestamp;
                dto.IsAnomaly = prediction[0] == 1;

                dto.Score = prediction[1];
                dto.PValue = prediction[2];
                dto.BatchNumber = reading.BatchNumber;
                dto.RoomName = reading.RoomName;

                // Filtering logic based on IsAnomaly parameter
                if (!filterDto.IsAnomaly.HasValue || dto.IsAnomaly == filterDto.IsAnomaly.Value)
                {
                    dtoList.Add(dto);
                }

            }

            return dtoList;
        }

        public class TempInput
        {
           public float Temperature { get; set; }
        }
    }
}
