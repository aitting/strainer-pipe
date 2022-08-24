using Hd.Mqtt;
using MQTTnet;
using Volo.Abp.DependencyInjection;

namespace Abp.StrainerPipe.MqttNetServer
{

    public class MqttMessageHandler : IMqttMessageHandler, ITransientDependency
    {

        private readonly IMqttMessageManager _mqttMessageManager;


        public MqttMessageHandler(IMqttMessageManager mqttMessageManager)
        {
            _mqttMessageManager = mqttMessageManager;
        }

        public virtual async Task HandAsync(MqttApplicationMessageReceivedEventArgs args)
        {

            await _mqttMessageManager.PutAsync(args.ApplicationMessage.Topic, args.ApplicationMessage.ConvertPayloadToString());
        }

        public virtual async Task HandleAsync(string topic, string message)
        {

            await _mqttMessageManager.PutAsync(topic, message);
        }
    }
}
