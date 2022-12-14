using Abp.StrainerPipe.Data;
using Abp.StrainerPipe.Transfer;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace Abp.StrainerPipe
{
    public abstract class LocalEventBusSource<T> : Source, ILocalEventHandler<EventBusSourceData<T>>, ITransientDependency where T : class
    {
        protected LocalEventBusSource(IAbpLazyServiceProvider abpLazyServiceProvider) : base(abpLazyServiceProvider)
        {
        }

        public virtual async Task HandleEventAsync(EventBusSourceData<T> eventData)
        {
            await ChannelTransfer.PutAsync(new ObjectMetadata(eventData.Data, eventData.TenantId));
        }
    }
}
