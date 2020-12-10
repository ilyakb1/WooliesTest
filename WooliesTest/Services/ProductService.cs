using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WooliesTest.Models;

namespace WooliesTest.Services
{
    public class ProductService : IProductService
    {
        private readonly IRecommendedProductsService recommendedProductsService;

        public ProductService(IRecommendedProductsService recommendedProductsService)
        {
            this.recommendedProductsService = recommendedProductsService ?? throw new System.ArgumentNullException(nameof(recommendedProductsService));
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
                return await recommendedProductsService.GetRecommendedProductsAsync(products);
            }

            return products;
        }


    }
}
