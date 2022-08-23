using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.StrainerPipe.Data
{
    public abstract class Metadata<T> : IMetadata<T> where T : notnull
    {
        public T Value { get; set; }

        public Metadata([NotNull] T value)
        {
            Value = value;
        }

        public virtual string Serialize()
        {
            return JsonConvert.SerializeObject(Value, Formatting.Indented);
        }

        public virtual T Deserialize([NotNull] string value)
        {
            return value.IsNullOrEmpty() ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public IMetadata<object> ToObject()
        {

            return new ObjectMetadata(Value);
        }
    }
}
