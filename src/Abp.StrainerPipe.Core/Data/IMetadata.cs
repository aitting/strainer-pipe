using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.StrainerPipe.Data
{
    public interface IMetadata<T>
    {
        T Value { get; }

        string Serialize();

        T Deserialize(string value);
    }
}
