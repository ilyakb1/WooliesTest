using System.Collections.Generic;
using System.Threading.Tasks;
using WooliesTest.Models;

namespace WooliesTest.Repositories
{
    public interface IDataRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();

        Task<IEnumerable<ShopperHistory>> GetShopperHistoriesAsync();
    }
}