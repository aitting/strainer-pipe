using System.Collections.Concurrent;
using Volo.Abp.EventBus;
using Volo.Abp.Guids;
using Volo.Abp.Threading;

namespace Abp.StrainerPipe
{
    public class MqttMessageManager : IMqttMessageManager
    {

        protected BlockingCollection<MqttMessageDto> Queue { get; set; }

        protected IGuidGenerator GuidGenerator { get; set; }

        private Thread _thread;

        private bool _started = false;

        protected IEventBus EventBus { get; }

        public MqttMessageManager(IGuidGenerator guidGenerator,
            IEventBus eventBus)
        {
            GuidGenerator = guidGenerator;
            EventBus = eventBus;
            Queue = new BlockingCollection<MqttMessageDto>(new ConcurrentQueue<MqttMessageDto>());

            _thread = new Thread(Process);
            _thread.IsBackground = true;
        }

        private void Process()
        {
            while (_started && !Queue.IsCompleted)
            {
                var message = Queue.Take();
                AsyncHelper.RunSync(() => EventBus.PublishAsync(message));
            }
        }

        public async Task PutAsync(string topic, string message)
        {
            await new TaskFactory().StartNew(() =>
            {
                if (!_started)
                {
                    _started = true;
                    _thread.Start();
                }
                Queue.Add(new MqttMessageDto(GuidGenerator.Create().ToString("N"), message, topic));
            });
        }


        public void Dispose()
        {
            _started = false;

            Queue?.Dispose();
        }
    }
}
