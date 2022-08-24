using Abp.StrainerPipe.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.StrainerPipe
{
    public interface ITypedSink<T> where T : notnull
    {


        Task<IMetadata<T>> ProcessAsync(IMetadata<T> data);
    }
}
