using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Abp.StrainerPipe.Data
{
    public class BlobMetadata : Metadata<byte[]>
    {

        public BlobMetadata()
        {

        }

        public BlobMetadata(byte[] value)
        {
            Value = value;
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
