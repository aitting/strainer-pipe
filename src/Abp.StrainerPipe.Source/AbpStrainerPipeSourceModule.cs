using Volo.Abp.Modularity;

namespace Abp.StrainerPipe
{

    [DependsOn(
        typeof(AbpStrainerPipeCoreModule),
        typeof(AbpStrainerPipeChannelModule)
        )]
    public class AbpStrainerPipeSourceModule : AbpModule
    {

    }
}