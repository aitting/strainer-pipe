using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace Abp.StrainerPipe
{


    public interface ISinkManager : ISingletonDependency, IDisposable
    {

        
        Task StartSinkAsync();

    }
}
