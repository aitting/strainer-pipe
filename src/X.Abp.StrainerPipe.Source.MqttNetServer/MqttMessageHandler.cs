using Hd.Mqtt;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MQTTnet;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace Abp.StrainerPipe.MqttNetServer
{

    public class MqttMessageHandler : IMqttMessageHandler, ITransientDependency
    {

        private readonly IMqttMessageManager _mqttMessageManager;
        public ILogger<MqttMessageHandler> Logger { get; set; }

        public ICurrentTenant CurrentTenant { get; set; }

        public MqttMessageHandler(IMqttMessageManager mqttMessageManager)
        {
            Logger = NullLogger<MqttMessageHandler>.Instance;
            _mqttMessageManager = mqttMessageManager;
        }
        // TODO: how to get tenantid?
        public virtual async Task HandAsync(MqttApplicationMessageReceivedEventArgs args)
        {

            await _mqttMessageManager.PutAsync(args.ApplicationMessage.Topic, args.ApplicationMessage.ConvertPayloadToString(), CurrentTenant.Id);
        }

        public virtual async Task HandleAsync(string topic, string message)
        {
            Logger.LogDebug(topic + " | " + message);
            await _mqttMessageManager.PutAsync(topic, message, CurrentTenant.Id);
        }
    }
}
