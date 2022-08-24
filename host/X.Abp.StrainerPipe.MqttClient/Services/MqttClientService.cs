using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using System.Threading;
using MQTTnet.Client.Options;
using MQTTnet;
using MQTTnet.Client;

namespace X.Abp.StrainerPipe.MqttClient.Services
{
    public class MqttClientService : ITransientDependency
    {

        public ILogger<MqttClientService> Logger { get; set; }

        private IMqttClient _mqttClient;

        private bool running = false;

        public MqttClientService()
        {
            Logger = NullLogger<MqttClientService>.Instance;
        }


        public async Task StartAsync()
        {
            var factory = new MqttFactory();
            var mqttClient = (_mqttClient = factory.CreateMqttClient());

            var options = new MqttClientOptionsBuilder()
                .WithProtocolVersion(MQTTnet.Formatter.MqttProtocolVersion.V500)
                .WithUserProperty("__tenant", Guid.NewGuid().ToString())
                .WithClientId(Guid.NewGuid().ToString())
                .WithCommunicationTimeout(TimeSpan.FromMilliseconds(1000 * 59))
                .WithWebSocketServer("ws://localhost:5169/mqtt")

                .Build();

            await mqttClient.ConnectAsync(options, CancellationToken.None);

            mqttClient.UseDisconnectedHandler(async e =>
            {
                Logger.LogInformation("### DISCONNECTED FROM SERVER ###");
                await Task.Delay(TimeSpan.FromSeconds(5));

                try
                {
                    await mqttClient.ConnectAsync(options, CancellationToken.None); // Since 3.0.5 with CancellationToken
                }
                catch
                {
                    Logger.LogInformation("### RECONNECTING FAILED ###");
                }
            });

            mqttClient.UseApplicationMessageReceivedHandler(e =>
            {
                Logger.LogInformation("### RECEIVED APPLICATION MESSAGE ###");
                Logger.LogInformation($"+ Topic = {e.ApplicationMessage.Topic}");
                Logger.LogInformation($"+ Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
                Logger.LogInformation($"+ QoS = {e.ApplicationMessage.QualityOfServiceLevel}");
                Logger.LogInformation($"+ Retain = {e.ApplicationMessage.Retain}");
                Logger.LogInformation("");

            });

            running = true;
            await SendAsync();
        }

        public async Task SendAsync()
        {

            while (running)
            {
                await Task.Delay(1000);
                var r = new Random().Next(1, 10);
                await _mqttClient.PublishAsync("realtime/data", $"实时数据：{r}");
                Logger.LogInformation($"实时数据：{r}");
            }
        }
    }
}
