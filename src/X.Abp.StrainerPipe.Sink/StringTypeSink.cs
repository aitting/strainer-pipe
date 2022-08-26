using Abp.StrainerPipe.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace Abp.StrainerPipe
{
    public abstract class StringTypeSink : Sink, ITypedSink<string>
    {

        
        public StringTypeSink(IAbpLazyServiceProvider lazyServiceProvider) : base(lazyServiceProvider)
        {
        }

        public override async Task<ObjectMetadata> ProcessAsync(ObjectMetadata data)
        {
            if (data.IsStringData())
            {
                return (ObjectMetadata)(await ProcessAsync(data.ToStringData())).ToObject();
            }

            return data;
        }

        public abstract Task<IMetadata<string>> ProcessAsync(IMetadata<string> data);
    }
}
