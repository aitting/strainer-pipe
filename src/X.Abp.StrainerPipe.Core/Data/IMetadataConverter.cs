using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace Abp.StrainerPipe.Data
{
    public interface IMetadataConverter : ITransientDependency
    {
        IMetadata<T> Convert<T>(T value,Guid? tenantId = null) where T : notnull;
    }
}
