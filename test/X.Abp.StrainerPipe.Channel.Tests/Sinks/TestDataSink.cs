using Abp.StrainerPipe.Data;
using Abp.StrainerPipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace X.Abp.StrainerPipe.Channel.Tests.Sinks
{
    public class TestDataSink : BlobTypeSink, ITransientDependency
    {

        protected DataSinkRecord Record => LazyServiceProvider.LazyGetRequiredService<DataSinkRecord>();

        public TestDataSink(IAbpLazyServiceProvider lazyServiceProvider)
            : base(lazyServiceProvider)
        {

        }

        public override Task<IMetadata<byte[]>> ProcessAsync(IMetadata<byte[]> data)
        {
            Record.Count++;
            return Task.FromResult(data);
        }
    }
}
