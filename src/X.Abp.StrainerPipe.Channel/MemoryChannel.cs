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
    public class MemoryChannel<T> : Channel<T> where T : notnull
    {
        protected ConcurrentQueue<T> Queue { get; set; }

        protected IMetadataConverter MetadataConverter { get; }

        public MemoryChannel(
            IAbpLazyServiceProvider abpLazyServiceProvider,
            IOptions<ChannelOptions> options,
            IMetadataConverter metadataConverter) : base(options, abpLazyServiceProvider)
        {
            Queue = new ConcurrentQueue<T>();
            MetadataConverter = metadataConverter;
        }

        public override void Dispose()
        {

        }

        public override async Task PutAsync(IMetadata<T> data)
        {
            await new TaskFactory().StartNew(() =>
            {
                Queue.Enqueue(data.Value);
                if (Queue.Count > Options.MaxChannelItemCount)
                {
                    TryDequeueMany(Queue.Count - Options.MaxChannelItemCount);
                }
            });
        }

        // TODO: 如何保证当前数据处理的事务性
        public override async Task<IEnumerable<IMetadata<T>>> TakeAsync(int count = 1)
        {

            List<IMetadata<T>> result = new List<IMetadata<T>>();
            for (int i = 0; i < count; i++)
            {
                try
                {
                    var md = Dequeue();
                    result.Add(md);
                }
                catch (Exception)
                {
                    break;
                }
            }

            return await Task.FromResult(result);
        }


        // TODO: 获取数据时 如何取得租户信息
        private IMetadata<T> Dequeue()
        {
            T data;
            if (Queue.TryDequeue(out data))
            {
                return MetadataConverter.Convert(data);
            }

            throw new Exception("Queue is empty");
        }

        private bool TryDequeue()
        {
            T data;
            return Queue.TryDequeue(out data);
        }

        private void TryDequeueMany(int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                TryDequeue();
            }
        }
    }
}
