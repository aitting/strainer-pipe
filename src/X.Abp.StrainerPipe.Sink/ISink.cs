using Abp.StrainerPipe.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.StrainerPipe
{
    public interface ISink : IDisposable
    {

        Task<ObjectMetadata> ProcessAsync(ObjectMetadata data);

        int Sort { get; }
    }
}
