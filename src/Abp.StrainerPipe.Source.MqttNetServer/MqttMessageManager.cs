using System.Collections.Concurrent;
using Volo.Abp.Guids;

namespace Abp.StrainerPipe
{
    public class MqttMessageManager : IMqttMessageManager
    {

        protected BlockingCollection<MqttMessageDto> Queue { get; set; }

        protected IGuidGenerator GuidGenerator { get; set; }

        public MqttMessageManager(IGuidGenerator guidGenerator)
        {
            GuidGenerator = guidGenerator;
            Queue = new BlockingCollection<MqttMessageDto>(new ConcurrentQueue<MqttMessageDto>());
        }

        public async Task PutAsync(string topic, string message)
        {
            await new TaskFactory().StartNew(() =>
            {
                Queue.Add(new MqttMessageDto(GuidGenerator.Create().ToString("N"), message, topic));
            });
        }

        public async Task<IEnumerable<MqttMessageDto>> TakeAsync(int count = 1)
        {
            return await Task.FromResult(Queue.Take(count));
        }

        public void Dispose()
        {
            Queue?.Dispose();
        }
    }
}
