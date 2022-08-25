using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace Abp.StrainerPipe
{
    public class ChannelSelector : ITransientDependency
    {

        public Dictionary<Type, object> Channels { get; private set; }

        public ChannelSelector(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            Channels = new Dictionary<Type, object>();
        }

        public IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// 根据数据类型获取对应的channel实例
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <returns></returns>
        public IChannel<T> Select<T>() where T : notnull
        {
            if (Channels.ContainsKey(typeof(T)))
            {
                return (IChannel<T>)Channels[typeof(T)];
            }

            var channel = ServiceProvider.GetRequiredService<IChannel<T>>();
            
            Channels.Add(typeof(T), channel);

            return channel;
        }
    }
}
