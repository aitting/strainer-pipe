using Abp.StrainerPipe.Data;
using Abp.StrainerPipe.Transfer;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace Abp.StrainerPipe
{

    public abstract class Source<T> : ISource where T : class
    {

        protected IChannelTransfer ChannelTransfer => LazyServiceProvider.LazyGetRequiredService<IChannelTransfer>();

        public Source(IAbpLazyServiceProvider abpLazyServiceProvider)
        {
            LazyServiceProvider = abpLazyServiceProvider;
        }

        public IAbpLazyServiceProvider LazyServiceProvider { get; set; }

        public ILogger<Source<T>> Logger => LazyServiceProvider.LazyGetRequiredService<ILogger<Source<T>>>();

        public IChannelManager ChannelManager => LazyServiceProvider.LazyGetRequiredService<IChannelManager>();

        public ISinkManagerFactory SinkManagerFactory => LazyServiceProvider.LazyGetRequiredService<ISinkManagerFactory>();

        public virtual async Task BeaforeSink(Guid? tenantId = null)
        {
            await SinkManagerFactory.CreateAndStartAsync(tenantId);
        }

        public virtual async Task HandleAsync([NotNull] T data, Guid? tenantId = null)
            
        {
            await ChannelTransfer.PutAsync(new ObjectMetadata(data, tenantId));
        }
    }
}
