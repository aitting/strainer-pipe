using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.StrainerPipe
{

    [Serializable]
    public class ChannelOptions
    {

        /// <summary>
        /// 控制channel队列中最大数据量
        /// </summary>
        public int MaxChannelItemCount { get; set; } = 10000;

        /// <summary>
        /// 超出最大队列数处理策略
        /// 0 清除最先进去的
        /// 1 清除之后进去的
        /// </summary>
        public int MaxChannelItemCountStrategy { get; set; }
    }
}
