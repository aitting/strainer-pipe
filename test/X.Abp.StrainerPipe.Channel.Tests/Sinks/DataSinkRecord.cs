using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace X.Abp.StrainerPipe.Channel.Tests.Sinks
{
    public class DataSinkRecord : ISingletonDependency
    {
        public int Count { get; set; }
    }
}
