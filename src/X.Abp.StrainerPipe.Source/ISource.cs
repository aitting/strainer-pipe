using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.StrainerPipe
{

    /// <summary>
    /// 
    /// </summary>
    public interface ISource : IDisposable
    {

        Task StartAsync();

        Task StopAsync();
    }
}
