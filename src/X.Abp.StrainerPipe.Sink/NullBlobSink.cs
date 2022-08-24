using Abp.StrainerPipe.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace Abp.StrainerPipe
{
    public class NullBlobSink : BlobTypeSink, ITransientDependency
    {
        public NullBlobSink(IAbpLazyServiceProvider lazyServiceProvider) : base(lazyServiceProvider)
        {
            Sort = 1;
        }


        public override async Task<IMetadata<byte[]>> ProcessAsync(IMetadata<byte[]> data)
        {
            Logger.LogInformation("NullBlobSink Process ...");
            Logger.LogInformation(data.Serialize());

            return await Task.FromResult(data);
        }
    }
}
