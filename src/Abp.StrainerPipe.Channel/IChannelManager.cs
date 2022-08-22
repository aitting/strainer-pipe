using Abp.StrainerPipe.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace Abp.StrainerPipe
{
    public interface IChannelManager : ITransientDependency
    {

        /// <summary>
        /// Puts the given data into the channel.
        /// </summary>
        /// <typeparam name="T">data type</typeparam>
        /// <param name="data">data</param>
        /// <returns></returns>
        Task PutAsync<T>(IMetadata<T> data);



        /// <summary>
        /// Returns the next data from the channel if available.
        /// If the channel does not have any datas available, this method must return null.
        /// </summary>
        /// <typeparam name="T">data type</typeparam>
        /// <param name="count">获取数量</param>
        /// <returns></returns>
        Task<IEnumerable<IMetadata<T>>> TakeAsync<T>(int count = 1);
    }
}
