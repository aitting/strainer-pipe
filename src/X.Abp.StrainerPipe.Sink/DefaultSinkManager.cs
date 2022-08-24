using Abp.StrainerPipe.Data;
using Abp.StrainerPipe.Transfer;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Threading;

namespace Abp.StrainerPipe
{
    public class DefaultSinkManager : ISinkManager
    {

        public SinkOptions Options { get; private set; }

        public IServiceProvider ServiceProvider { get; private set; }

        private Lazy<List<Sink>> _sinks;
        private readonly Lazy<List<DataTaker>> _dataTakers;

        protected virtual List<Sink> Sinks => _sinks.Value;
        protected virtual List<DataTaker> DataTakers => _dataTakers.Value;


        protected AbpAsyncTimer Timer { get; private set; }


        protected int SingleTakeCount { get; set; } = 1000;

        public DefaultSinkManager(
            IServiceProvider serviceProvider,
            IOptions<SinkOptions> options,
            AbpAsyncTimer timer)
        {
            ServiceProvider = serviceProvider;
            Options = options.Value;


            _sinks = new Lazy<List<Sink>>(() =>
            {
                return Options.Sinks.Select(t => (Sink)ServiceProvider.GetService(t)).OrderBy(s => s.Sort).ToList();
            }, true);

            _dataTakers = new Lazy<List<DataTaker>>(() => Options.DataTakers.Select(x => (DataTaker)ServiceProvider.GetService(x)).ToList(), true);

            timer.Period = 1000 * 10;
            timer.Elapsed = RunAsync;
            Timer = timer;
        }

        public void Dispose()
        {
            Timer?.Stop();
        }

        public async Task StartSinkAsync()
        {
            await new TaskFactory().StartNew(() => Timer.Start());
        }

        private async Task RunAsync(AbpAsyncTimer timer)
        {
            foreach (var dataTaker in DataTakers)
            {
                var data = await dataTaker.TakeObjectAsync(SingleTakeCount);

                foreach (var item in data)
                {
                    // TODO: 流式执行每一个Sink
                    foreach (var sink in Sinks)
                    {
                        var lastData = sink.ProcessAsync(item);

                    }
                }


            }


        }
    }
}
