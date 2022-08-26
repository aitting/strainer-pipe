using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.StrainerPipe.Data
{
    public static class IMetadataExtensions
    {
        public static bool IsStringData(this IMetadata<object> metadata)
        {
            return metadata.Value.GetType() == typeof(string);
        }

        public static bool IsBlobData(this IMetadata<object> metadata)
        {
            return metadata.Value.GetType() == typeof(byte[]);
        }

        public static StringMetadata ToStringData(this IMetadata<object> metadata)
        {
            return new StringMetadata(metadata.Serialize(), metadata.TenantId);
        }

        public static BlobMetadata ToBlobData(this IMetadata<object> metadata)
        {
            try
            {
                return new BlobMetadata((byte[])metadata.Value, metadata.TenantId);
            }
            catch (Exception)
            {

                return new BlobMetadata(System.Text.Encoding.UTF8.GetBytes(metadata.Serialize()));
            }
        }
    }
}
