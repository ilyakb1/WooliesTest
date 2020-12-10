using Newtonsoft.Json;
using System.Threading.Tasks;
using WooliesTest.Models;
using WooliesTest.Services;
using Xunit;

namespace WooliesTestTests
{
    public class TrolleyCalculatorTests
    {
        [Fact]
        public async Task Calculate_Trolley_MinTotalAmount_1()
        {
            var json = ResourseHelper.GetEmbeddedResourceAsString("TestFiles.trolley1.json");
            var trolley = JsonConvert.DeserializeObject<Trolley>(json);

            var calculator = new TrolleyCalculator();
            var totalValue = await calculator.CalculateAsync(trolley);

            Assert.Equal(250, totalValue);
        }

        [Fact]
        public async Task Calculate_Trolley_MinTotalAmount_2()
        {
            var json = ResourseHelper.GetEmbeddedResourceAsString("TestFiles.trolley2.json");
            var trolley = JsonConvert.DeserializeObject<Trolley>(json);

            var calculator = new TrolleyCalculator();
            var totalValue = await calculator.CalculateAsync(trolley);

            Assert.Equal(14, totalValue);
        }
    }
}

