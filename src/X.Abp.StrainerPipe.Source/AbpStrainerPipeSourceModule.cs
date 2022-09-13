using Abp.StrainerPipe.Transfer;
using Volo.Abp.EventBus;
using Volo.Abp.Modularity;

namespace Abp.StrainerPipe
{

    [DependsOn(
        typeof(AbpStrainerPipeSourceContractsModule),
        typeof(AbpEventBusModule),
        typeof(AbpStrainerPipeChannelTransferModule)
        )]
    public class AbpStrainerPipeSourceModule : AbpModule
    {

    }
}