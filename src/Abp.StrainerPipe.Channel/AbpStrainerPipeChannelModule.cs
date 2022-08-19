using Volo.Abp.Modularity;

namespace Abp.StrainerPipe.Channel
{

    [DependsOn(typeof(AbpStrainerPipeCoreModule))]
    public class AbpStrainerPipeChannelModule : AbpModule
    {

    }
}