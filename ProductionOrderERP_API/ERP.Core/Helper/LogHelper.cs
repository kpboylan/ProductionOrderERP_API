using Serilog;
using System.Text;

namespace ProductionOrderERP_API.ERP.Core.Helper
{
    public static class LogHelper
    {
        public static void LogControllerError(string source, string message)
        {
            Log.Error(FormatControllerErrorMessage(source, message));
        }

        public static void LogServiceError(string source, string message)
        {
            Log.Error(FormatServiceErrorMessage(source, message));
        }

        public static void LogInformation(string source, string message)
        {
            Log.Information(FormatServiceErrorMessage(source, message));
        }

        private static string FormatControllerErrorMessage(string source, string message)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Error occurred in {0} Controller: {1}", source, message);

            return sb.ToString();
        }

        private static string FormatServiceErrorMessage(string source, string message)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Error occurred in {0} Service: {1}", source, message);

            return sb.ToString();
        }
    }
}
