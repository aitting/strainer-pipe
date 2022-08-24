using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.StrainerPipe
{
    public class EventBusSourceData<T> where T : class
    {

        private EventBusSourceData() { }

        public EventBusSourceData(T data)
        {
            Data = data;
        }

        public T Data { get; set; }
    }
}
