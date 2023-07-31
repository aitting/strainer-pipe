using Abp.StrainerPipe.Data;
using Abp.StrainerPipe.Transfer;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Threading;

namespace Abp.StrainerPipe.MqttNetServer
{
    public class MqttNetServerSource : LocalEventBusSource<MqttMessageData>, ITransientDependency
    {
        public ICurrentTenant CurrentTenant => LazyServiceProvider.LazyGetRequiredService<ICurrentTenant>();

        public MqttNetServerSource(
            IAbpLazyServiceProvider abpLazyServiceProvider) : base(abpLazyServiceProvider)
        {

        }

        public override async Task HandleAsync(MqttMessageData data, Guid? tenantId = null)
        {
            await ChannelTransfer.PutAsync(new BlobMetadata(data.GetBytes(), data.TenantId));
        }
    }
}
