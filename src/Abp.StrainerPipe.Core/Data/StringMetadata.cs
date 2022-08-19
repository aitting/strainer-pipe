using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.StrainerPipe.Data
{
    public class StringMetadata : Metadata<string>
    {

        public StringMetadata()
        {
            Value = string.Empty;
        }

        public StringMetadata(string value)
        {
            Value = value;
        }


        public override string Serialize()
        {
            return Value;
        }


        public override string Deserialize(string value)
        {

            return value;
        }
    }
}
