using Abp.StrainerPipe.Data;
using Abp.StrainerPipe.Transfer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Threading;

namespace Abp.StrainerPipe
{
    public class DefaultSinkManager : ISinkManager
    {

        public SinkOptions Options { get; private set; }

        public IServiceScopeFactory ServiceScopeFactory { get; private set; }

        private Lazy<List<Sink>> _sinks;
        private readonly Lazy<List<DataTaker>> _dataTakers;

        protected virtual List<Sink> Sinks => _sinks.Value;
        protected virtual List<DataTaker> DataTakers => _dataTakers.Value;

        protected ILoggerFactory LoggerFactory => LazyServiceProvider.LazyGetRequiredService<ILoggerFactory>();

        protected ILogger Logger => LazyServiceProvider.LazyGetService<ILogger>(provider => LoggerFactory?.CreateLogger(GetType().FullName) ?? NullLogger.Instance);

        public IAbpLazyServiceProvider LazyServiceProvider { get; set; }

        protected AbpAsyncTimer Timer { get; private set; }


        protected int SingleTakeCount { get; set; } = 1000;

        public DefaultSinkManager(
            IServiceProvider serviceProvider,
            AbpAsyncTimer timer,
            IOptions<SinkOptions> options,
            IServiceScopeFactory serviceScopeFactory)
        {

            Options = options.Value;

            ServiceScopeFactory = serviceScopeFactory;

            _sinks = new Lazy<List<Sink>>(() =>
            {
                return Options.Sinks.Select(t => (Sink)serviceProvider.GetService(t)).OrderBy(s => s.Sort).ToList();
            }, true);

            _dataTakers = new Lazy<List<DataTaker>>(() => Options.DataTakers.Select(x => (DataTaker)serviceProvider.GetService(x)).ToList(), true);

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

            using (var scope = ServiceScopeFactory.CreateScope())
            {
                try
                {
                    var sinkRunner = scope.ServiceProvider.GetService<SinkRunner>();
                    if (sinkRunner == null)
                    {
                        Logger.LogWarning("Cannot Create SinkRunner Instance!");
                        return;
                    }

                    foreach (var dataTaker in DataTakers)
                    {
                        var data = await dataTaker.TakeObjectAsync(SingleTakeCount);

                        foreach (var item in data)
                        {
                            await sinkRunner.StartAsync(Sinks, item);
                        }
                    }
                }
                catch (Exception ex)
                {

                    await scope.ServiceProvider
                         .GetRequiredService<IExceptionNotifier>()
                         .NotifyAsync(new ExceptionNotificationContext(ex));

                    Logger.LogException(ex);
                }
            }

        }
    }
}
