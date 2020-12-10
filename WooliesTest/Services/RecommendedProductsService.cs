using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WooliesTest.Models;

namespace WooliesTest.Services
{
    public class RecommendedProductsService : IRecommendedProductsService
    {
        private readonly IShopperHistoryService shopperHistoryService;

        public RecommendedProductsService(IShopperHistoryService shopperHistoryService)
        {
            this.shopperHistoryService = shopperHistoryService ?? throw new System.ArgumentNullException(nameof(shopperHistoryService));
        }

        public async Task<IEnumerable<Product>> GetRecommendedProductsAsync(IEnumerable<Product> products)
        {
            var productShopFrequency = await shopperHistoryService.GetProductShopFrequencyAsync();
            return SortProductsBasedOnShoppingHistory(products, productShopFrequency);
        }

        IEnumerable<Product> SortProductsBasedOnShoppingHistory(
            IEnumerable<Product> products,
            Dictionary<string, int> productShopFrequency)
        {
            var recommendProducts = products.Select(p =>
            {
                return new RecommendProduct()
                {
                    Product = p,
                    Popularity = productShopFrequency.ContainsKey(p.Name) ? productShopFrequency[p.Name] : 0
                };
            });

            return recommendProducts.OrderByDescending(p => p.Popularity).Select(p => p.Product);
        }
    }
}
