using Abp.StrainerPipe.Transfer;
using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp.Guids;
using Volo.Abp.Modularity;
using X.Abp.StrainerPipe.Sink.Abstractions;

namespace Abp.StrainerPipe
{

    [DependsOn(
        typeof(AbpGuidsModule),
        typeof(AbpStrainerPipeSinkAbstractionsModule),
        typeof(AbpStrainerPipeChannelTransferModule))]
    public class AbpStrainerPipeSinkModule : AbpModule
    {

        public override async Task OnApplicationInitializationAsync(Volo.Abp.ApplicationInitializationContext context)
        {
            try
            {
                // 添加默认处理（无租户）
                using (var scope = context.ServiceProvider.CreateScope())
                {
                    await scope.ServiceProvider.GetRequiredService<ISinkManagerFactory>()
                        .CreateAndStartAsync();
                }
            }
            catch (Exception ex)
            {
                //Ignore
            }            
        }

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
