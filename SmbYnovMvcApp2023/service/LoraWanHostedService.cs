using Models;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using Ynov2023AppDemo.Data;

namespace SmbYnovMvcApp2023.service
{
    public class LoraWanHostedService : IHostedService
    {
        private Timer _timer = null;
        private HttpClient _httpClient;
        private const string H_URL_TTN = @"https://eu1.cloud.thethings.network/api/v3/as/applications/soulmbengue-app-lorawansrv-1/packages/storage/uplink_message";
        private const string H_API_TOKEN =  "NNSXS.AFXIMSE6QXHFGBFXSYHMQQ6XFXJKDAOKRNFGHHI.N4WWBDZ7B7TNJA4IKJ6DGZAS6PNSRQBXZSWPFZT5ZSON52NGJW2A";
        private IServiceProvider _serviceProvider;
        private ILogger _logger;

        public LoraWanHostedService(ILogger<LoraWanHostedService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization =  new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", H_API_TOKEN);

            _timer = new Timer(FaireLeJob, null, TimeSpan.FromSeconds(10), TimeSpan.FromMinutes(5));
            
            return Task.CompletedTask;
        }

        private void FaireLeJob(object? state)
        {
            try
            {
                var lastAccessed = DateTimeOffset.MinValue;

                using (var scope = _serviceProvider.CreateScope())
                {
                    ServiceStorageIntegration service = scope.ServiceProvider.GetService<ServiceStorageIntegration>();
                    lastAccessed = service.GetLastAccessed(H_URL_TTN);
                }

                var uriBuilder = new UriBuilder(H_URL_TTN);
                if (lastAccessed != DateTimeOffset.MinValue)
                    uriBuilder.Query = "after=" + lastAccessed.ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ");

                var taskResponse = _httpClient.GetAsync(uriBuilder.ToString());

                if (taskResponse == null || taskResponse.Result == null)
                {
                    // Probleme
                }
                else
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        ServiceStorageIntegration service = scope.ServiceProvider.GetService<ServiceStorageIntegration>();

                        var httpRspMsg = taskResponse.Result;
                        if (taskResponse.Result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var stringUpLinks = httpRspMsg?.Content.ReadAsStringAsync().Result;
                            var strJeaningString = stringUpLinks?.Split("\n", StringSplitOptions.RemoveEmptyEntries);
                            foreach (var link in strJeaningString)
                            {
                                if (!string.IsNullOrEmpty(link))
                                {
                                    var upLinkMessage = JsonConvert.DeserializeObject<TTMlorawanResult>(link);
                                    service.ManageUpLinkMessage(upLinkMessage, H_URL_TTN);    
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //logger l'exception

            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
