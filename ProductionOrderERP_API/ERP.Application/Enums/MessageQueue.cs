using System.ComponentModel;

namespace ProductionOrderERP_API.ERP.Application.Enums
{
    public class MessageQueue
    {
        public enum MessageQueueName
        {
            [Description("AddMaterialQueue")]
            AddMaterialQueue,

            [Description("UpdateMaterialQueue")]
            UpdateMaterialQueue,
        }

        public enum MessageHostName
        {
            [Description("Localhost")]
            LocalHost,
        }
    }
}
