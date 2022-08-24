using Abp.StrainerPipe.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace Abp.StrainerPipe
{
    public abstract class BlobTypeSink : Sink, ITypedSink<byte[]>
    {
        protected BlobTypeSink(IAbpLazyServiceProvider lazyServiceProvider) : base(lazyServiceProvider)
        {
        }

        public override async Task<ObjectMetadata> ProcessAsync(ObjectMetadata data)
        {
            if (data.IsBlobData())
            {
                return (ObjectMetadata)(await ProcessAsync(data.ToBlobData())).ToObject();
            }

            return data;
        }

        public abstract Task<IMetadata<byte[]>> ProcessAsync(IMetadata<byte[]> data);
    }
}
