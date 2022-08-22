using Abp.StrainerPipe.Data;
using Abp.StrainerPipe.Transfer;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Threading;

namespace Abp.StrainerPipe.MqttNetServer
{
    public class MqttNetServerSource : Source, ISingletonDependency
    {

        private readonly IMqttMessageManager _mqttMessageManager;

        protected AbpAsyncTimer Timer { get; set; }

        protected IChannelTransfer ChannelTransfer => LazyServiceProvider.LazyGetRequiredService<IChannelTransfer>();

        public MqttNetServerSource(
            IAbpLazyServiceProvider abpLazyServiceProvider,
            IMqttMessageManager mqttMessageManager,
            AbpAsyncTimer timer) : base(abpLazyServiceProvider)
        {
            timer.Period = 1000;
            timer.Elapsed = Runner;
            Timer = timer;
            _mqttMessageManager = mqttMessageManager;
        }

        public override void Dispose()
        {
            _mqttMessageManager?.Dispose();
        }

        private async Task Runner(AbpAsyncTimer timer)
        {
            var data = await _mqttMessageManager.TakeAsync();
            foreach (var item in data)
            {
                if (item != null)
                {
                    await ChannelTransfer.PutAsync(new BlobMetadata(item.GetBytes()));
                }
            }
        }

        public override async Task StartAsync()
        {
            await new TaskFactory().StartNew(() => Timer.Start());
        }

        public override async Task StopAsync()
        {
            await new TaskFactory().StartNew(() => Timer?.Stop());
        }
    }
}
