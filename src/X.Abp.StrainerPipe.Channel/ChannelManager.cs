using Abp.StrainerPipe.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace Abp.StrainerPipe
{


    public class ChannelManager : IChannelManager
    {

        public IAbpLazyServiceProvider AbpLazyServiceProvider { get; set; }

        protected ChannelSelector Selector => AbpLazyServiceProvider.LazyGetRequiredService<ChannelSelector>();

        public ChannelManager(IAbpLazyServiceProvider serviceProvider)
        {
            AbpLazyServiceProvider = serviceProvider;
        }

        public async Task PutAsync<T>(IMetadata<T> data) where T : notnull
        {
            var channel = Selector.Select<T>();
            await channel.PutAsync(data);
        }

        public async Task<IEnumerable<IMetadata<T>>> TakeAsync<T>(int count = 1) where T : notnull
        {
            var channel = Selector.Select<T>();
            return await channel.TakeAsync(count);
        }
    }
}
