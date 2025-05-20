using Microsoft.ML.Data;

namespace ProductionOrderERP_API.ERP.Core.Entity
{
    public class SensorPrediction
    {
        [VectorType(3)]
        public double[] Prediction { get; set; }
    }
}
