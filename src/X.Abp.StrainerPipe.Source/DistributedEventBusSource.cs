using Abp.StrainerPipe.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace Abp.StrainerPipe
{
    public abstract class DistributedEventBusSource<T> : Source, IDistributedEventHandler<EventBusSourceData<T>>, ITransientDependency where T : class
    {
        protected DistributedEventBusSource(IAbpLazyServiceProvider abpLazyServiceProvider) : base(abpLazyServiceProvider)
        {
        }

        public async Task HandleEventAsync(EventBusSourceData<T> eventData)
        {
            await ChannelTransfer.PutAsync(new ObjectMetadata(eventData.Data, eventData.TenantId));
        }
    }
}
