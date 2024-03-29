﻿using System.Collections.Concurrent;
using Volo.Abp.EventBus;
using Volo.Abp.EventBus.Local;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Threading;

namespace Abp.StrainerPipe
{
    public class MqttMessageManager : IMqttMessageManager
    {

        protected BlockingCollection<MqttMessageData> Queue { get; set; }

        protected IGuidGenerator GuidGenerator { get; set; }

        private Thread _thread;

        private bool _started = false;

        protected ILocalEventBus EventBus { get; }


        public MqttMessageManager(
            IGuidGenerator guidGenerator,
            ILocalEventBus eventBus)
        {
            GuidGenerator = guidGenerator;
            EventBus = eventBus;
            Queue = new BlockingCollection<MqttMessageData>(new ConcurrentQueue<MqttMessageData>());

            _thread = new Thread(Process);
            _thread.IsBackground = true;
        }

        private void Process()
        {
            while (_started && !Queue.IsCompleted)
            {
                var message = Queue.Take();
                AsyncHelper.RunSync(() => EventBus.PublishAsync(new EventBusSourceData<MqttMessageData>(message, message.TenantId)));
                Thread.Sleep(100);
            }
        }

        public async Task PutAsync(string topic, string message, Guid? tenantId = null)
        {
            await new TaskFactory().StartNew(() =>
            {
                if (!_started)
                {
                    _started = true;
                    _thread.Start();
                }
                Queue.Add(new MqttMessageData(GuidGenerator.Create().ToString("N"), message, topic, tenantId));
            });
        }


        public void Dispose()
        {
            _started = false;

            Queue?.Dispose();
        }
    }
}
