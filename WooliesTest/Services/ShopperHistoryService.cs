using System.Collections.Generic;
using System.Threading.Tasks;
using WooliesTest.Models;
using WooliesTest.Repositories;

namespace WooliesTest.Services
{
    public class ShopperHistoryService : IShopperHistoryService
    {
        private readonly IDataRepository repository;

        public ShopperHistoryService(IDataRepository repository)
        {
            this.repository = repository ?? throw new System.ArgumentNullException(nameof(repository));
        }

        public async Task<Dictionary<string, int>> GetProductShopFrequencyAsync()
        {
            var shopperHistories = await repository.GetShopperHistoriesAsync();
            return CalculateProductShopFrequency(shopperHistories);
        }

        Dictionary<string, int> CalculateProductShopFrequency(IEnumerable<ShopperHistory> shopperHistories)
        {
            var history = new Dictionary<string, int>();

            foreach (var shopperHistory in shopperHistories)
            {
                foreach (var product in shopperHistory.Products)
                {
                    if (!history.ContainsKey(product.Name))
                    {
                        history.Add(product.Name, 0);
                    }

                    history[product.Name]++;
                }
            }

            return history;
        }
    }
}

