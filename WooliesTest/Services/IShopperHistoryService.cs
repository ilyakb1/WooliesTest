using System.Collections.Generic;
using System.Threading.Tasks;

namespace WooliesTest.Services
{
    public interface IShopperHistoryService
    {
        Task<Dictionary<string, int>> GetProductShopFrequencyAsync();
    }
}