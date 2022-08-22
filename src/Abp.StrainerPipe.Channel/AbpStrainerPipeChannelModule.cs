using Abp.StrainerPipe.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Caching;
using Volo.Abp.Modularity;

namespace Abp.StrainerPipe
{

    [DependsOn(
        typeof(AbpCachingModule),
        typeof(AbpStrainerPipeCoreModule))]
    public class AbpStrainerPipeChannelModule : AbpModule
    {

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            context.Services.AddMemoryChannels();

            Configure<ChannelOptions>(configuration.GetSection("Strainer:Channel"));
        }
    }
}