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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual async Task PutAsync<T>(IMetadata<T> data) where T : notnull
        {
            await _channelManager.PutAsync(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<IMetadata<T>>> TakeAsync<T>(int count = 1) where T : notnull
        {
            return await _channelManager.TakeAsync<T>(count);
        }
    }
}
