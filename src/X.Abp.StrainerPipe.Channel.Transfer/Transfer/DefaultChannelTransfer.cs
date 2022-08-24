using Abp.StrainerPipe.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.StrainerPipe.Transfer
{
    public class DefaultChannelTransfer : IChannelTransfer
    {

        private readonly IChannelManager _channelManager;

        public DefaultChannelTransfer(IChannelManager channelManager)
        {
            _channelManager = channelManager;
        }

        public virtual async Task PutAsync<T>(IMetadata<T> data)
        {
            await _channelManager.PutAsync(data);
        }

        public virtual async Task<IEnumerable<IMetadata<T>>> TakeAsync<T>(int count = 1)
        {
            return await _channelManager.TakeAsync<T>(count);
        }
    }
}
