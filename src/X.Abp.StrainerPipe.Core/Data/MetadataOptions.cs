using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.StrainerPipe.Data
{
    public class MetadataOptions
    {
        public Dictionary<Type, Type> MetadataTypes { get; private set; }

        public MetadataOptions()
        {
            MetadataTypes = new Dictionary<Type, Type>();
        }

        public void AddMetadataType<TValue, TInstance>() where TInstance : IMetadata<TValue>
        {
            MetadataTypes.Add(typeof(TValue), typeof(TInstance));
        }
    }
}
