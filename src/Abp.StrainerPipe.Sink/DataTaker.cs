using Abp.StrainerPipe.Data;
using Abp.StrainerPipe.Transfer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.StrainerPipe
{
    public abstract class DataTaker
    {

        protected DataTaker(IChannelTransfer channelTransfer)
        {
            ChannelTransfer = channelTransfer;
        }

        protected IChannelTransfer ChannelTransfer { get; }

        public abstract Task<IEnumerable<IMetadata<object>>> TakeObjectAsync(int count = 1);


        public abstract Type DataType { get; }
    }
}
