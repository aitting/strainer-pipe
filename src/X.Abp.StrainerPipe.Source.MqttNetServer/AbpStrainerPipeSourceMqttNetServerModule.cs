using Hd.Mqtt;
using Volo.Abp.Guids;
using Volo.Abp.Modularity;

namespace Abp.StrainerPipe.MqttNetServer
{


    [DependsOn(
        typeof(AbpGuidsModule),
        typeof(AbpStrainerPipeSourceModule),
        typeof(Hd.Mqtt.HdMqttModule)
        )]
    public class AbpStrainerPipeSourceMqttNetServerModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<MqttServerOptions>(options =>
            {
                options.MessageHandlers.Add<MqttMessageHandler>();
            });
        }
    }
}