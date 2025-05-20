namespace ProductionOrderERP_API.ERP.Application.UseCase.Room
{
    public class HumidityProcessingException : Exception
    {
        public HumidityProcessingException(string message, Exception innerException)
        : base(message, innerException)
        {
        }
    }
}
