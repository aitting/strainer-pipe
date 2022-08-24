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
               

        public override Task<ObjectMetadata> ProcessAsync(ObjectMetadata data)
        {
            Logger.LogInformation("NullSink Process ...");
            Logger.LogInformation(data.Serialize());
            return Task.FromResult(data);
        }
    }
}
