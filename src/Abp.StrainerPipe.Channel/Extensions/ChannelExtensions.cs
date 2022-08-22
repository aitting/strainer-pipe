using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Modularity;

namespace Abp.StrainerPipe.Extensions
{
    public static class ChannelExtensions
    {
        public static void AddMemoryChannels(this IServiceCollection services)
        {
            services.Add(ServiceDescriptor.Singleton(typeof(IChannel<>), typeof(MemoryChannel<>)));
        }


        public static void AddChannels(this IServiceCollection services, AbpModule abpModule)
        {
            //services.Add(ServiceDescriptor.Singleton(typeof(IChannel<>), typeof(MemoryChannel<>)));
            var types = abpModule.GetType().Assembly.GetTypes()
                .Where(x => x.IsGenericType)
                .Where(x => x.IsAssignableFrom(typeof(IChannel<>)));
            foreach (var type in types)
            {
                services.Replace(ServiceDescriptor.Singleton(typeof(IChannel<>), type));
            }
        }
    }
}
