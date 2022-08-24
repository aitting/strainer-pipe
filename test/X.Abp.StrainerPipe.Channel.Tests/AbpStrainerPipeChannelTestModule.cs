using Abp.StrainerPipe.TestBase;
using Volo.Abp.Modularity;

namespace Abp.StrainerPipe.Channel.Tests
{
    [DependsOn(
        typeof(AbpStrainerPipeTestBaseModule), 
        typeof(AbpStrainerPipeChannelModule))]
    public class AbpStrainerPipeChannelTestModule : AbpModule
    {


    }
}