using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Abp.StrainerPipe
{
    public class MqttMessageDto
    {
        public MqttMessageDto(string id, string message, string topic)
        {
            Id = id;
            Message = message;
            Topic = topic;
        }

        public string Id { get; set; }

        public string Message { get; set; }

        public string Topic { get; set; }


    }

    public static class MqttMessageDtoExtensions
    {

        public static byte[] GetBytes(this MqttMessageDto dto)
        {
            return System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dto));
        }
    }
}
