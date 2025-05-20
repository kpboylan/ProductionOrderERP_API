namespace ProductionOrderERP_API.ERP.Application.UseCase
{
    public abstract class ServiceBase
    {
        protected async Task<T> ExecuteWithHandlingAsync<T>(Func<Task<T>> action, string contextName)
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                Core.Helper.LogHelper.LogServiceError(contextName, ex.ToString());

                throw new ServiceExecutionException($"An error occurred in {contextName}.", ex);
            }
        }
    }

    public class ServiceExecutionException : Exception
    {
        public ServiceExecutionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
