using Abp.StrainerPipe.Data;
using Abp.StrainerPipe.TestBase;
using Newtonsoft.Json.Linq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Xunit;

namespace Abp.StrainerPipe.Channel.Tests
{
    

    public class ChannelManagerTests : AbpStrainerPipeChannelTestBase
    {

        protected IChannelManager _channelManager { get; }

        public ChannelManagerTests()
        {
            _channelManager = GetRequiredService<IChannelManager>();
        }

        [Fact]
        public async Task PutTest()
        {
            await _channelManager.PutAsync(new StringMetadata("我是一条数据:1"));
            await _channelManager.PutAsync(new StringMetadata("我是一条数据:2"));
            await _channelManager.PutAsync(new StringMetadata("我是一条数据:3"));
            await _channelManager.PutAsync(new StringMetadata("我是一条数据:4"));
            await _channelManager.PutAsync(new StringMetadata("我是一条数据:5"));
            await _channelManager.PutAsync(new StringMetadata("我是一条数据:6"));
            await _channelManager.PutAsync(new StringMetadata("我是一条数据:7"));
            await _channelManager.PutAsync(new StringMetadata("我是一条数据:8"));
            await _channelManager.PutAsync(new StringMetadata("我是一条数据:9"));
            await _channelManager.PutAsync(new StringMetadata("我是一条数据:10"));

            var strDataList = await _channelManager.TakeAsync<string>(200);
            strDataList.Count().ShouldBe(10);

            var strDataList2 = await _channelManager.TakeAsync<string>(100);
            strDataList2.Count().ShouldBe(0);

            strDataList.First().Value.ShouldBe("我是一条数据:1");
            strDataList.Last().Value.ShouldBe("我是一条数据:10");

            var data1 = System.Text.Encoding.UTF8.GetBytes("我是一条数据:1");
            var data2 = System.Text.Encoding.UTF8.GetBytes("我是一条数据:2");
            var data3 = System.Text.Encoding.UTF8.GetBytes("我是一条数据:3");
            var data4 = System.Text.Encoding.UTF8.GetBytes("我是一条数据:4");

            await _channelManager.PutAsync(new BlobMetadata(data1));
            await _channelManager.PutAsync(new BlobMetadata(data2));
            await _channelManager.PutAsync(new BlobMetadata(data3));
            await _channelManager.PutAsync(new BlobMetadata(data4));

            var byteDataList = await _channelManager.TakeAsync<byte[]>(3);

            byteDataList.Count().ShouldBe(3);

            byteDataList.First().Serialize().ShouldBe("我是一条数据:1");
            byteDataList.Last().Serialize().ShouldBe("我是一条数据:3");


        }
    }
}
