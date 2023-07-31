using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Abp.StrainerPipe
{
    public interface ISinkManagerFactory : ISingletonDependency
    {
        ISinkManager GetOrCreate(Guid? tenantId = null, bool createNew = false);

        Task CreateAndStartAsync(Guid? tenantId = null);
    }
}
