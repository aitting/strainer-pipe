using Abp.StrainerPipe;
using Abp.StrainerPipe.Channel.Tests;
using Abp.StrainerPipe.Data;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace X.Abp.StrainerPipe.Channel.Tests.Sinks
{
    public class DataSinkTests : AbpStrainerPipeChannelTestBase
    {
        protected IChannelManager _channelManager { get; }

        protected DataSinkRecord Record { get; }

        public DataSinkTests()
        {
            _channelManager = GetRequiredService<IChannelManager>();
            Record = GetRequiredService<DataSinkRecord>();
        }

        [Fact]
        public async Task TestSink()
        {
            Guid tenantId = new Guid("49464a6a-f6e2-0e5f-e21f-3a04e4919153");
            await _channelManager.PutAsync(new StringMetadata("我是一条数据:1", tenantId));
            await _channelManager.PutAsync(new StringMetadata("我是一条数据:2", tenantId));
            await _channelManager.PutAsync(new StringMetadata("我是一条数据:3", tenantId));
            await _channelManager.PutAsync(new StringMetadata("我是一条数据:4", tenantId));
            await _channelManager.PutAsync(new StringMetadata("我是一条数据:5", tenantId));
            await _channelManager.PutAsync(new StringMetadata("我是一条数据:6", tenantId));
            await _channelManager.PutAsync(new StringMetadata("我是一条数据:7", tenantId));
            await _channelManager.PutAsync(new StringMetadata("我是一条数据:8", tenantId));
            await _channelManager.PutAsync(new StringMetadata("我是一条数据:9", tenantId));
            await _channelManager.PutAsync(new StringMetadata("我是一条数据:10", tenantId));

            Record.Count.ShouldBe(10);
        }
    }
}
