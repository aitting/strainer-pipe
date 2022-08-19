using Newtonsoft.Json;
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

        public virtual string Serialize()
        {
            return JsonConvert.SerializeObject(Value, Formatting.Indented);
        }

        public virtual T Deserialize(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
