using Abp.StrainerPipe.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace Abp.StrainerPipe
{
    public abstract class Sink : ISink
    {

        public string Id { get; set; }

        public ICurrentTenant CurrentTenant => LazyServiceProvider.LazyGetRequiredService<ICurrentTenant>();

        public ILogger<Sink> Logger => LazyServiceProvider.LazyGetRequiredService<ILogger<Sink>>();

        protected Sink(IAbpLazyServiceProvider lazyServiceProvider)
        {
            LazyServiceProvider = lazyServiceProvider;
            Id = lazyServiceProvider.LazyGetRequiredService<IGuidGenerator>().Create().ToString("N");
        }

        public IAbpLazyServiceProvider LazyServiceProvider { get; private set; }

        public int Sort { get; set; }

        public virtual void Dispose()
        {

        }

        public abstract Task<ObjectMetadata> ProcessAsync(ObjectMetadata data);

    }
}
