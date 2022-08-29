using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace Abp.StrainerPipe
{


    public interface ISinkManager : ITransientDependency, IDisposable
    {

        
        Task StartSinkAsync(Guid? tenantId = null);

    }
}
