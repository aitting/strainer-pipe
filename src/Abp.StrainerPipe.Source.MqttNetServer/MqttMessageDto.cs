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
}
