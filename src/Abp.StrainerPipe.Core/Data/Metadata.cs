using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.StrainerPipe.Data
{
    public abstract class Metadata<T> : IMetadata<T> 
    {
        public T Value { get; set; }

        public Metadata()
        {
#pragma warning disable CS8601 // 引用类型赋值可能为 null。
            Value = default;
#pragma warning restore CS8601 // 引用类型赋值可能为 null。
        }

        public Metadata(T value)
        {
            Value = value;
        }
    }
}
