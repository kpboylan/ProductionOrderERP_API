using System.ComponentModel;

namespace ProductionOrderERP_API.ERP.Application.Enums
{
    public class MessageQueue
    {
        public enum MessageQueueName
        {
            [Description("CreateMaterial")]
            CreateMaterial,

            [Description("UpdateMaterial")]
            UpdateMaterial,

            [Description("DeleteMaterial")]
            DeleteMaterial,
        }

        public enum MessageHostName
        {
            [Description("Localhost")]
            LocalHost,
        }
    }
}
