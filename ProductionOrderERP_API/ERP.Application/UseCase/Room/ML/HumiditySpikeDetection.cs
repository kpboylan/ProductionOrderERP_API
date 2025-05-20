using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Core.Entity;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.TimeSeries;

namespace ProductionOrderERP_API.ERP.Application.UseCase.Room.ML
{
    public class HumiditySpikeDetection
    {
        public List<HumidityDtoResponse> ProcessData(List<HumidityDtoResponse> dataList, HumidityDtoRequest filterDto)
        {
            var mlContext = new MLContext();

            var mlInputList = dataList
                .Select(r => new HumidityInput { Humidity = (float)r.Humidity })
                .ToList();

            var dataView = mlContext.Data.LoadFromEnumerable(mlInputList);

            // Define the spike detection pipeline
            var pipeline = mlContext.Transforms.DetectIidSpike(
                outputColumnName: nameof(HumidityPrediction.Prediction),
                inputColumnName: nameof(HumidityInput.Humidity),
                confidence: 95,
                pvalueHistoryLength: 20);

            var model = pipeline.Fit(dataView);
            var transformed = model.Transform(dataView);

            var predictions = mlContext.Data
                .CreateEnumerable<HumidityPrediction>(transformed, reuseRowObject: false)
                .ToList();

            // Map predictions to response DTOs
            var response = new List<HumidityDtoResponse>();
            for (int i = 0; i < dataList.Count; i++)
            {
                var reading = dataList[i];
                var prediction = predictions[i].Prediction;

                
                if ((bool)filterDto.IsSpike) // Return Spikes only
                {
                    if (prediction[0] == 1)
                    {
                        response.Add(new HumidityDtoResponse
                        {
                            Timestamp = reading.Timestamp,
                            Humidity = reading.Humidity,
                            IsSpike = prediction[0] == 1,
                            Score = prediction[1],
                            PValue = prediction[2],
                            BatchNumber = reading.BatchNumber,
                            RoomName = reading.RoomName
                        });
                    }
                }
                else // Return everything
                {
                    response.Add(new HumidityDtoResponse
                    {
                        Timestamp = reading.Timestamp,
                        Humidity = reading.Humidity,
                        IsSpike = prediction[0] == 1,
                        Score = prediction[1],
                        PValue = prediction[2],
                        BatchNumber = reading.BatchNumber,
                        RoomName = reading.RoomName
                    });
                }
            }

            return response;
        }

        private class HumidityInput
        {
            public float Humidity { get; set; }
        }

        private class HumidityPrediction
        {
            [VectorType(3)]
            public double[] Prediction { get; set; }
        }
    }
}
