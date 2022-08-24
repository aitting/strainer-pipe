using Abp.StrainerPipe.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Abp.StrainerPipe
{
    public class SinkRunner : ITransientDependency
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public SinkRunner(
            IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;

            ExecutedSinkIds = new List<string>();
            Sinks = new List<Sink>();
        }

        protected virtual List<Sink> Sinks { get; set; }

        private List<string> ExecutedSinkIds;

        public async Task StartAsync(List<Sink> sinks, ObjectMetadata data)
        {

            if (sinks == null) throw new AbpException("sinks cannot be null");

            Sinks = sinks.OrderBy(x => x.Sort).ToList();
            ExecutedSinkIds = new List<string>();

            var sink = Sinks.First();
            var dataNext = await sink.ProcessAsync(data);

            ExecutedSinkIds.Add(sink.Id);

            await NextAsync(sink, dataNext);
        }


        public async Task NextAsync(Sink lastSink, ObjectMetadata data)
        {
            var nextSink = NextSink(lastSink);
            if (nextSink != null)
            {
                var dataNext = await nextSink.ProcessAsync(data);
                ExecutedSinkIds.Add(nextSink.Id);
                await NextAsync(nextSink, dataNext);
            }
        }

        private Sink NextSink(Sink current)
        {
            return Sinks.Where(x => x.Sort >= current.Sort && !ExecutedSinkIds.Contains(x.Id)).FirstOrDefault();
        }
    }
}
