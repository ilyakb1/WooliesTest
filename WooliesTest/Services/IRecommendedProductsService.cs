using System.Collections.Generic;
using System.Threading.Tasks;
using WooliesTest.Models;

namespace WooliesTest.Services
{
    public interface IRecommendedProductsService
    {
        Task<IEnumerable<Product>> GetRecommendedProductsAsync(IEnumerable<Product> products);
    }
}