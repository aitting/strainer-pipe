using System;
using Volo.Abp.Json;
using Volo.Abp.Modularity;

namespace Abp.StrainerPipe
{

    /// <summary>
    /// 1. 定义数据传输标准
    /// 
    /// </summary>
    [DependsOn(typeof(AbpJsonModule))]
    public class AbpStrainerPipeCoreModule : AbpModule
    {

    }
}
