using Abp.StrainerPipe.Transfer;
using Volo.Abp.Modularity;

namespace Abp.StrainerPipe
{

    [DependsOn(
        typeof(AbpStrainerPipeChannelTransferModule)
        )]
    public class AbpStrainerPipeSourceModule : AbpModule
    {

    }
}