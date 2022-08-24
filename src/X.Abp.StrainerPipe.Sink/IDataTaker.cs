using Abp.StrainerPipe.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.StrainerPipe
{
    public interface IDataTaker<T>
    {
        Task<IEnumerable<IMetadata<T>>> TakeAsync(int count = 1);
    }
}
