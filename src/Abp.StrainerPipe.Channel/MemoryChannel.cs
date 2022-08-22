using Abp.StrainerPipe.Data;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace Abp.StrainerPipe
{
    /// <summary>
    /// 内存存储
    /// 
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public class MemoryChannel<T> : Channel<T>
    {
        protected BlockingCollection<T> Queue { get; set; }

        protected IMetadataConverter MetadataConverter { get; }

        protected ChannelOptions Options { get; set; }

        public MemoryChannel(
            IOptions<ChannelOptions> options,
            IMetadataConverter metadataConverter)
        {
            Queue = new BlockingCollection<T>(new ConcurrentQueue<T>());
            Options = options.Value;

            MetadataConverter = metadataConverter;
        }

        public override void Dispose()
        {
            Queue.Dispose();
        }

        public override async Task PutAsync(IMetadata<T> data)
        {
            await new TaskFactory().StartNew(() =>
            {
                Queue.Add(data.Value);
                if (Queue.Count > Options.MaxChannelItemCount)
                {
                    if (Options.MaxChannelItemCountStrategy == 0)
                    {
                        Queue.Take(Queue.Count - Options.MaxChannelItemCount);
                    }

                    if (Options.MaxChannelItemCountStrategy == 1)
                    {
                        Queue.TakeLast(Queue.Count - Options.MaxChannelItemCount);
                    }
                }
            });
        }

        public override async Task<IEnumerable<IMetadata<T>>> TakeAsync(int count = 1)
        {

            return await Task.FromResult(
                Queue.Take(count).Select(x =>
                    MetadataConverter.Convert(x)
                ));
        }
    }
}
