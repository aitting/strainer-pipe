using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Abp.StrainerPipe
{
    public class MqttMessageData
    {
        public MqttMessageData(string id, string message, string topic, Guid? tenantId = null)
        {
            Id = id;
            Message = message;
            Topic = topic;
            TenantId = tenantId;
        }

        public string Id { get; set; }

        public string Message { get; set; }

        public string Topic { get; set; }

        public Guid? TenantId { get; set; }
    }

    public static class MqttMessageDtoExtensions
    {

        public static byte[] GetBytes(this MqttMessageData dto)
        {
            return System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dto));
        }
    }
}
