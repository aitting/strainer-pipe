﻿using Abp.StrainerPipe.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace Abp.StrainerPipe
{
    public abstract class Channel<T> : IChannel<T> where T : notnull
    {
        public abstract void Dispose();
        public abstract Task PutAsync(IMetadata<T> data);
        public abstract Task<IEnumerable<IMetadata<T>>> TakeAsync(int count = 1);

        public IAbpLazyServiceProvider LazyServiceProvider { get; set; }

        protected ICurrentTenant CurrentTenant => LazyServiceProvider.LazyGetRequiredService<ICurrentTenant>();

        protected ChannelOptions Options { get; set; }

        public ILogger<Channel<T>> Logger => LazyServiceProvider.LazyGetRequiredService<ILogger<Channel<T>>>();

        public Channel(
            IOptions<ChannelOptions> options,
            IAbpLazyServiceProvider abpLazyServiceProvider)
        {
            Options= options.Value;

            LazyServiceProvider = abpLazyServiceProvider;
        }
    }
}
