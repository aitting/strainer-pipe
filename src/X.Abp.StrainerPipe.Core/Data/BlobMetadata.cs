﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Abp.StrainerPipe.Data
{
    public class BlobMetadata : Metadata<byte[]>
    {

        
        public BlobMetadata(byte[] value, Guid? tenantId = null) :base(value,tenantId)
        {
             
        }

        public override string Serialize()
        {
            return System.Text.Encoding.UTF8.GetString(Value);
        }


        public override byte[] Deserialize(string value)
        {

            return System.Text.Encoding.UTF8.GetBytes(value);
        }
    }
}
