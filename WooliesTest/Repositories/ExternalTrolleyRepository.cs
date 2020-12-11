using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WooliesTest.Models;

namespace WooliesTest.Services
{
    public class ExternalTrolleyRepository : ITrolleyCalculator
    {
        readonly HttpClient client;
        private readonly ILogger<ExternalTrolleyRepository> logger;
        readonly string baseUrl;
        readonly string token;

        public ExternalTrolleyRepository(IConfiguration configuration, HttpClient client, ILogger<ExternalTrolleyRepository> logger)
        {
            if (configuration == null)
            {
                throw new System.ArgumentNullException(nameof(configuration));
            }

            this.client = client ?? throw new System.ArgumentNullException(nameof(client));
            this.logger = logger ?? throw new System.ArgumentNullException(nameof(logger));

            baseUrl = configuration["BaseUrl"];
            token = configuration["Token"];
        }

        public async Task<decimal?> CalculateAsync(Trolley trolley)
        {
            var url = $"{baseUrl}/trolleyCalculator?token={token}";

            var json = JsonConvert.SerializeObject(trolley);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, stringContent);

            if (response.IsSuccessStatusCode)
            {
                var text = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<decimal>(text);
            }
            else
            {
                string errorMessage = $"Post to {url} return error with code '{response.StatusCode}' and message {response.ReasonPhrase}";
                logger.LogError(errorMessage);
                throw new ApiException(errorMessage);
            }
        }
    }
}
