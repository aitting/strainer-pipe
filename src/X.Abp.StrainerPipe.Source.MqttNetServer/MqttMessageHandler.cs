using Hd.Mqtt;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MQTTnet;
using Volo.Abp.DependencyInjection;

namespace Abp.StrainerPipe.MqttNetServer
{

    public class MqttMessageHandler : IMqttMessageHandler, ITransientDependency
    {

        private readonly IMqttMessageManager _mqttMessageManager;
        public ILogger<MqttMessageHandler> Logger { get; set; }

        public MqttMessageHandler(IMqttMessageManager mqttMessageManager)
        {
            Logger = NullLogger<MqttMessageHandler>.Instance;
            _mqttMessageManager = mqttMessageManager;
        }

        public virtual async Task HandAsync(MqttApplicationMessageReceivedEventArgs args)
        {

            await _mqttMessageManager.PutAsync(args.ApplicationMessage.Topic, args.ApplicationMessage.ConvertPayloadToString());
        }

        public virtual async Task HandleAsync(string topic, string message)
        {
            Logger.LogDebug(topic + " | " + message);
            await _mqttMessageManager.PutAsync(topic, message);
        }
    }
}
