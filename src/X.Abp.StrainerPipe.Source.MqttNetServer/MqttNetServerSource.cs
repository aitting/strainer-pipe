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


        public override async Task HandleEventAsync(EventBusSourceData<MqttMessageDto> eventData)
        {
            await ChannelTransfer.PutAsync(new BlobMetadata(eventData.Data.GetBytes()));
        }
    }
}
