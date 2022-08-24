using Abp.StrainerPipe.Transfer;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace Abp.StrainerPipe
{

    public abstract class Source : ISource
    {

        protected IChannelTransfer ChannelTransfer => LazyServiceProvider.LazyGetRequiredService<IChannelTransfer>();

        public Source(IAbpLazyServiceProvider abpLazyServiceProvider)
        {
            LazyServiceProvider = abpLazyServiceProvider;
        }

        public IAbpLazyServiceProvider LazyServiceProvider { get; set; }

        public ILogger<Source> Logger => LazyServiceProvider.LazyGetRequiredService<ILogger<Source>>();

        public IChannelManager ChannelManager => LazyServiceProvider.LazyGetRequiredService<IChannelManager>();

         

    }
}
