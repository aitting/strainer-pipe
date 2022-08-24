using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.StrainerPipe.Data
{
    public interface IMetadata<T> where T : notnull
    {
        T Value { get; }

        string Serialize();

        T Deserialize(string value);

        IMetadata<object> ToObject();
    }
}
