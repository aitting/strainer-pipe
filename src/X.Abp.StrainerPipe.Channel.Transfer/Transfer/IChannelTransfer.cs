using Abp.StrainerPipe.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace Abp.StrainerPipe.Transfer
{
    public interface IChannelTransfer : ITransientDependency
    {

        Task PutAsync<T>(IMetadata<T> data);

        Task<IEnumerable<IMetadata<T>>> TakeAsync<T>(int count = 1);
    }
}
