using Abp.StrainerPipe.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace Abp.StrainerPipe
{
    public abstract class Sink : ISink
    {

        public ILogger<Sink> Logger => LazyServiceProvider.LazyGetRequiredService<ILogger<Sink>>();

        protected Sink(IAbpLazyServiceProvider lazyServiceProvider)
        {
            LazyServiceProvider = lazyServiceProvider;
        }

        public IAbpLazyServiceProvider LazyServiceProvider { get; private set; }

        public int Sort { get; set; }

        public virtual void Dispose()
        {

        }

        public abstract Task<IMetadata<object>> ProcessAsync(IMetadata<object> data);

        public Task ProcessAsync<T>(IMetadata<T> data)
        {
            throw new NotImplementedException();
        }
    }
}
