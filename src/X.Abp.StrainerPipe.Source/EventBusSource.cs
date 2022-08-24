using Abp.StrainerPipe.Data;
using Abp.StrainerPipe.Transfer;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace Abp.StrainerPipe
{
    public abstract class EventBusSource<T> : Source, ILocalEventHandler<EventBusSourceData<T>> where T : class
    {
        protected EventBusSource(IAbpLazyServiceProvider abpLazyServiceProvider) : base(abpLazyServiceProvider)
        {
        }

        public async Task HandleEventAsync(EventBusSourceData<T> eventData)
        {
            await ChannelTransfer.PutAsync(new ObjectMetadata(eventData.Data));
        }
    }
}
