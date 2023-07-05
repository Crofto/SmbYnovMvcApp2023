using MQTTnet;
using MQTTnet.Client;

namespace SmbYnovMvcApp2023.service
{
    public class MQTTHostedService : IHostedService
    {
        private ILogger _logger;
        private Timer _timer = null;
        private IServiceProvider _serviceProvider;

        private MqttClientOptions _mqttOptions;
        private IMqttClient _mqttClient;
        private readonly string _mqttBrokerAddress = "test.mosquitto.org";
        private readonly string _mqttClientId = "ynov-lyon-2023-esp32-reymbaut-tmtcidUnique";
        private readonly string _mqttTopic = "topic/ynov-lyon-2023/esp32/reymbaut/in";
        const int mqttPort = 1883;

        public MQTTHostedService(ILogger<MQTTHostedService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var mqttFactory = new MqttFactory();

            _mqttClient = mqttFactory.CreateMqttClient();            
            var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithClientId(_mqttClientId)
                .WithTcpServer(_mqttBrokerAddress, mqttPort).Build();

            _mqttClient.ConnectedAsync += MqttClient_ConnectedAsync;

            _mqttClient.ConnectAsync(mqttClientOptions);


            return Task.CompletedTask;
            
        }



        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        private Task MqttClient_ConnectedAsync(MqttClientConnectedEventArgs arg)
        {
            var topicFilter = new MqttTopicFilterBuilder()
                .WithTopic(_mqttTopic)
                .Build();
            _mqttClient.SubscribeAsync(topicFilter);

            _mqttClient.ApplicationMessageReceivedAsync += HandleMqttMessageReceived;

            return Task.CompletedTask;
        }

        private Task HandleMqttMessageReceived(MqttApplicationMessageReceivedEventArgs e)
        {
            // Traitement du message MQTT reçu
            string topic = e.ApplicationMessage.Topic;
            try { 
                string payload = System.Text.Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

                Console.WriteLine($"Message MQTT reçu. Topic : {topic}. Payload : {payload}");

                using (var scope = _serviceProvider.CreateScope())
                {
                    ServiceStorageIntegration service = scope.ServiceProvider.GetService<ServiceStorageIntegration>();
                    service.InsertTemp(payload);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }

            // Ajoutez ici votre logique de traitement du message

            return Task.CompletedTask;
        }

    }
}
