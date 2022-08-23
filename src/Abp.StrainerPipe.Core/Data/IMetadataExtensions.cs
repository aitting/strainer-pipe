using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.StrainerPipe.Data
{
    public static class IMetadataExtensions
    {
        public static StringMetadata ToStringData(this IMetadata<object> metadata)
        {
            return new StringMetadata(metadata.Serialize());
        }

        public static BlobMetadata ToBlobData(this IMetadata<object> metadata)
        {
            try
            {
                return new BlobMetadata((byte[])metadata.Value);
            }
            catch (Exception)
            {

                return new BlobMetadata(System.Text.Encoding.UTF8.GetBytes(metadata.Serialize()));
            }
        }
    }
}
