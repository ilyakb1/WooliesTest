using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WooliesTest.Models;
using WooliesTest.Repositories;

namespace WooliesTest.Services
{
    public class ProductService : IProductService
    {
        private readonly IDataRepository repository;

        public ProductService(IDataRepository repository)
        {
            this.repository = repository ?? throw new System.ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<Product>> SortAsync(IEnumerable<Product> products, ProductSortingOptions sortingOption)
        {
            if (sortingOption == ProductSortingOptions.Low)
            {
                return products.OrderBy(p => p.Price);
            }

            if (sortingOption == ProductSortingOptions.High)
            {
                return products.OrderByDescending(p => p.Price);
            }

            if (sortingOption == ProductSortingOptions.Ascending)
            {
                return products.OrderBy(p => p.Name);
            }

            if (sortingOption == ProductSortingOptions.Descending)
            {
                return products.OrderByDescending(p => p.Name);
            }

            if (sortingOption == ProductSortingOptions.Recommended)
            {
                var shopperHistories = await repository.GetShopperHistoriesAsync();
                return GetRecommendedProducts(products, shopperHistories);
            }

            return products;
        }

        public static IEnumerable<Product> GetRecommendedProducts(IEnumerable<Product> products, IEnumerable<ShopperHistory> shopperHistories)
        {
            var productBuyFrequency = GetProductBuyFrequencyAsync(shopperHistories);

            var recommendProducts = products.Select(p =>
            {
                return new RecommendProduct()
                {
                    Product = p,
                    Popularity = productBuyFrequency.ContainsKey(p.Name) ? productBuyFrequency[p.Name] : 0
                };
            });

            return recommendProducts.OrderByDescending(p => p.Popularity).Select(p => p.Product);
        }

        static Dictionary<string, int> GetProductBuyFrequencyAsync(IEnumerable<ShopperHistory> shopperHistories)
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
