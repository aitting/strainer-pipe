﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.StrainerPipe.Data
{
    public class ObjectMetadata : IMetadata<object>
    {
        public ObjectMetadata(object value, Guid? tenantId = null)
        {
            Value = value;
            TenantId = tenantId;
        }


        public object Value { get; set; }

        public Guid? TenantId { get; set; }

        public object Deserialize(string value)
        {
            return value;
        }

        public string Serialize()
        {
            return Value.ToString();
        }

        public IMetadata<object> ToObject()
        {
            return new ObjectMetadata(Value);
        }
    }
}
