using System.Collections.Generic;
using System.Threading.Tasks;
using WooliesTest.Models;

namespace WooliesTest.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> SortAsync(IEnumerable<Product> products, ProductSortingOptions sortingOption);
    }
}