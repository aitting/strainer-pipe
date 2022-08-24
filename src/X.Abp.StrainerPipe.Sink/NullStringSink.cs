using Abp.StrainerPipe.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace Abp.StrainerPipe
{
    public class NullStringSink : StringTypeSink, ITransientDependency
    {
        public NullStringSink(IAbpLazyServiceProvider lazyServiceProvider) : base(lazyServiceProvider)
        {
            Sort = 1;
        }

        public override async Task<IMetadata<string>> ProcessAsync(IMetadata<string> data)
        {
            Logger.LogInformation("NullStringSink Process ...");
            Logger.LogInformation(data.Serialize());

            return await Task.FromResult(data);
        }
    }
}
