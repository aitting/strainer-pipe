using Abp.StrainerPipe.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.StrainerPipe
{
    public interface ISink : IDisposable
    {

        Task ProcessAsync<T>(IMetadata<T> data);

        int Sort { get; }
    }
}
