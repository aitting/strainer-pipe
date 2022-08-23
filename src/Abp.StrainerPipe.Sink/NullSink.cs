using Abp.StrainerPipe.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace Abp.StrainerPipe
{
    public class NullSink : Sink, ITransientDependency
    {
        public NullSink(IAbpLazyServiceProvider lazyServiceProvider) : base(lazyServiceProvider)
        {
        }

        public override async Task ProcessAsync<T>(IMetadata<T> data)
        {
            Logger.LogInformation("NullSink Process ...");
            Logger.LogInformation(data.Serialize());
            await Task.CompletedTask;
        }
    }
}
