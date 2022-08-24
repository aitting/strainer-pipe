using Abp.StrainerPipe.Data;
using Abp.StrainerPipe.Transfer;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.Threading;

namespace Abp.StrainerPipe.MqttNetServer
{
    public class MqttNetServerSource : EventBusSource<MqttMessageDto>, ITransientDependency
    {

        public MqttNetServerSource(
            IAbpLazyServiceProvider abpLazyServiceProvider) : base(abpLazyServiceProvider)
        {

        }


        public async Task HandleEventAsync(MqttMessageDto eventData)
        {
            await ChannelTransfer.PutAsync(new BlobMetadata(eventData.GetBytes()));
        }
    }
}
