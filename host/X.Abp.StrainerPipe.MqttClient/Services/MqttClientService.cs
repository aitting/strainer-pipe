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
using Volo.Abp.Timing;
using System.Text.Json.Serialization;
using Volo.Abp.Json;

namespace X.Abp.StrainerPipe.MqttClient.Services
{
    public class MqttClientService : ITransientDependency
    {

        public ILogger<MqttClientService> Logger { get; set; }

        private IMqttClient _mqttClient;

        private bool running = false;

        protected IClock Clock { get; set; }

        protected IJsonSerializer JsonSerializer { get; }

        public MqttClientService(IClock clock,
            IJsonSerializer jsonSerializer)
        {
            Logger = NullLogger<MqttClientService>.Instance;
            Clock = clock;
            JsonSerializer = jsonSerializer;
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
                .WithWebSocketServer("ws://localhost:44354/mqtt")

                .Build();

            await mqttClient.ConnectAsync(options, CancellationToken.None);

            mqttClient.UseDisconnectedHandler(async e =>
            {
                Logger.LogInformation("### DISCONNECTED FROM SERVER ###");
                running = false;
                await Task.Delay(TimeSpan.FromSeconds(5));

                try
                {
                    await mqttClient.ConnectAsync(options, CancellationToken.None); // Since 3.0.5 with CancellationToken
                    running = true;
                    await SendAsync();
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

                var data = new { Name = $"T{r}", Value = r, Time = Clock.Now, Group = "test" };
                var message = JsonSerializer.Serialize(data);

                await _mqttClient.PublishAsync("realtime/test", message);
                Logger.LogInformation($"实时数据：{r}");
            }
        }
    }
}
