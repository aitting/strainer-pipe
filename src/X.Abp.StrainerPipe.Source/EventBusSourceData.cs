using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.StrainerPipe
{
    public class EventBusSourceData<T> where T : class
    {

        private EventBusSourceData() { }

        public EventBusSourceData(T data, Guid? tenantId = null)
        {
            Data = data;
            TenantId = tenantId;
        }

        public T Data { get; set; }

        public Guid? TenantId { get; set; }
    }
}
