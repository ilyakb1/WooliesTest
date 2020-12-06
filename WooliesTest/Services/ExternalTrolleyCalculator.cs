using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WooliesTest.Models;

namespace WooliesTest.Services
{
    public class ExternalTrolleyCalculator : ITrolleyCalculator
    {
        readonly HttpClient client;
        readonly string baseUrl;
        readonly string token;

        public ExternalTrolleyCalculator(IConfiguration configuration, HttpClient client)
        {
            if (configuration == null)
            {
                throw new System.ArgumentNullException(nameof(configuration));
            }

            this.client = client ?? throw new System.ArgumentNullException(nameof(client));

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

            return null;
        }
    }
}
