using Abp.StrainerPipe.Data;
using Abp.StrainerPipe.Transfer;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Threading;

namespace Abp.StrainerPipe.MqttNetServer
{
    public class MqttNetServerSource : EventBusSource<MqttMessageData>, ITransientDependency
    {
        public ICurrentTenant CurrentTenant => LazyServiceProvider.LazyGetRequiredService<ICurrentTenant>();

        public MqttNetServerSource(
            IAbpLazyServiceProvider abpLazyServiceProvider) : base(abpLazyServiceProvider)
        {

        }


        public override async Task HandleEventAsync(EventBusSourceData<MqttMessageData> eventData)
        {
            using (CurrentTenant.Change(eventData.Data.TenantId))
            {
                await ChannelTransfer.PutAsync(new BlobMetadata(eventData.Data.GetBytes(), eventData.Data.TenantId));
            }
        }
    }
}
