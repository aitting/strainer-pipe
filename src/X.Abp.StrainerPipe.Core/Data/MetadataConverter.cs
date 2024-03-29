﻿using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.StrainerPipe.Data
{
    public class MetadataConverter : IMetadataConverter
    {

        protected MetadataOptions Options { get; set; }
        public MetadataConverter(IOptions<MetadataOptions> options)
        {
            Options = options.Value;
        }


        public IMetadata<T> Convert<T>(T value, Guid? tenantId = null) where T : notnull
        {
            if (Options.MetadataTypes.ContainsKey(typeof(T)))
            {
                var type = Options.MetadataTypes[typeof(T)];
                return (IMetadata<T>)System.Activator.CreateInstance(type, value, tenantId);
            }

            return (IMetadata<T>)new StringMetadata(value?.ToString() ?? "", tenantId);
        }
    }
}
