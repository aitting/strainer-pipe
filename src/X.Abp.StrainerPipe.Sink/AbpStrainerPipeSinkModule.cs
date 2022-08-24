using Abp.StrainerPipe.Transfer;
using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp.Guids;
using Volo.Abp.Modularity;

namespace Abp.StrainerPipe
{

    [DependsOn(
        typeof(AbpGuidsModule),
        typeof(AbpStrainerPipeChannelTransferModule))]
    public class AbpStrainerPipeSinkModule : AbpModule
    {

        public override void ConfigureServices(ServiceConfigurationContext context)
        {

            List<Type> sinks = new List<Type>();
            List<Type> dataTakers = new List<Type>();
            context.Services.OnRegistred(ctx =>
            {
                if (ctx.ImplementationType.IsAssignableTo(typeof(Sink)))
                {
                    sinks.AddIfNotContains(ctx.ImplementationType);
                }

                if (ctx.ImplementationType.IsAssignableTo(typeof(DataTaker)))
                {
                    dataTakers.AddIfNotContains(ctx.ImplementationType);
                }
            });

            Configure<SinkOptions>(options =>
            {
                foreach (var type in sinks)
                {
                    options.Sinks.Add(type);
                }

                foreach (var item in dataTakers)
                {
                    options.DataTakers.Add(item);
                }
            });
        }
    }
}
