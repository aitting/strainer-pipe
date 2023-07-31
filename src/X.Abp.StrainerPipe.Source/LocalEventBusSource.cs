using Abp.StrainerPipe.Data;
using Abp.StrainerPipe.Transfer;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace Abp.StrainerPipe
{
    public abstract class LocalEventBusSource<T> : Source<T>, ILocalEventHandler<EventBusSourceData<T>>, ITransientDependency where T : class
    {
        protected LocalEventBusSource(IAbpLazyServiceProvider abpLazyServiceProvider) : base(abpLazyServiceProvider)
        {
        }

        public async Task HandleEventAsync(EventBusSourceData<T> eventData)
        {
            await BeaforeSink(eventData.TenantId);
            await HandleAsync(eventData.Data, eventData.TenantId);
        }
    }
}
