using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WooliesTest.Models;

namespace WooliesTest.Repositories
{
    public class DataRepository : IDataRepository
    {
        readonly HttpClient client;
        private readonly ILogger<DataRepository> logger;
        readonly string baseUrl;
        readonly string token;

        public DataRepository(IConfiguration configuration, HttpClient client, ILogger<DataRepository> logger)
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

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var productApiUrl = $"{baseUrl}/products?token={token}";

            var response = await client.GetAsync(productApiUrl);
            if (response.IsSuccessStatusCode)
            {
                var text = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Product>>(text);
            }
            else
            {
                string errorMessage = $"Get from {productApiUrl} return error with code '{response.StatusCode}' and message {response.ReasonPhrase}";
                logger.LogError(errorMessage);
                throw new ApiException(errorMessage);
            }
        }

        public async Task<IEnumerable<ShopperHistory>> GetShopperHistoriesAsync()
        {
            var shopperHistoryApiUrl = $"{baseUrl}/shopperHistory?token={token}";

            var response = await client.GetAsync(shopperHistoryApiUrl);
            if (response.IsSuccessStatusCode)
            {
                var text = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<ShopperHistory>>(text);
            }
            else
            {
                string errorMessage = $"Get from {shopperHistoryApiUrl} return error with code '{response.StatusCode}' and message {response.ReasonPhrase}";
                logger.LogError(errorMessage);
                throw new ApiException(errorMessage);
            }
        }
    }
}
