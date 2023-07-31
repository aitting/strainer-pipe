using Abp.StrainerPipe.TestBase;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Modularity;
using Volo.Abp.Threading;

namespace Abp.StrainerPipe.Channel.Tests
{
    [DependsOn(
        typeof(AbpStrainerPipeSinkModule),
        typeof(AbpStrainerPipeTestBaseModule), 
        typeof(AbpStrainerPipeChannelModule))]
    public class AbpStrainerPipeChannelTestModule : AbpModule
    {

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            Guid tenantId = new Guid("49464a6a-f6e2-0e5f-e21f-3a04e4919153");
            AsyncHelper.RunSync(() => context.ServiceProvider.GetRequiredService<ISinkManagerFactory>().CreateAndStartAsync(tenantId));
        }
    }
}