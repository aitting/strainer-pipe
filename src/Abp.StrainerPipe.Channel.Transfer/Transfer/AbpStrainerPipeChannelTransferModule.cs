using Volo.Abp.Modularity;

namespace Abp.StrainerPipe.Transfer
{


    [DependsOn(typeof(AbpStrainerPipeChannelModule))]
    public class AbpStrainerPipeChannelTransferModule : AbpModule
    {

    }
}