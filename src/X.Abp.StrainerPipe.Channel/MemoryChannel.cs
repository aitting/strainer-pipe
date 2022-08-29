using Abp.StrainerPipe.Data;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
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

        protected Dictionary<string, ConcurrentQueue<T>> Queues { get; set; }

        protected IMetadataConverter MetadataConverter { get; }

        public MemoryChannel(
            IAbpLazyServiceProvider abpLazyServiceProvider,
            IOptions<ChannelOptions> options,
            IMetadataConverter metadataConverter) : base(options, abpLazyServiceProvider)
        {

            Queues = new Dictionary<string, ConcurrentQueue<T>>();
            MetadataConverter = metadataConverter;
        }

        public override void Dispose()
        {

        }

        private ConcurrentQueue<T> GetQueue()
        {

            string dicKey = "host";
            if (CurrentTenant.Id.HasValue)
            {
                dicKey = CurrentTenant.Id.Value.ToString("N");
            }

            if (!Queues.ContainsKey(dicKey))
            {
                Queues.Add(dicKey, new ConcurrentQueue<T>());
            }

            return Queues[dicKey];
        }

        public override async Task PutAsync(IMetadata<T> data)
        {
            var queue = GetQueue();
            await new TaskFactory().StartNew(() =>
            {
                queue.Enqueue(data.Value);
                if (queue.Count > Options.MaxChannelItemCount)
                {
                    TryDequeueMany(queue.Count - Options.MaxChannelItemCount);
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
            var queue = GetQueue();
            T data;
            if (queue.TryDequeue(out data))
            {
                return MetadataConverter.Convert(data);
            }

            throw new Exception("Queue is empty");
        }

        private bool TryDequeue()
        {
            var queue = GetQueue();
            T data;
            return queue.TryDequeue(out data);
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
