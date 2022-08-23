using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Collections;

namespace Abp.StrainerPipe
{
    public class SinkOptions
    {
        public ITypeList<Sink> Sinks { get; private set; }
        public ITypeList<DataTaker> DataTakers { get; private set; }

        public SinkOptions()
        {
            Sinks = new TypeList<Sink>();
            DataTakers = new TypeList<DataTaker>();
        }
    }
}
