using System.Threading.Tasks;
using WooliesTest.Models;

namespace WooliesTest.Services
{
    public interface ITrolleyCalculator
    {
        Task<decimal?> CalculateAsync(Trolley trolley);
    }
}