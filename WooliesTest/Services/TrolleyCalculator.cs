using System.Linq;
using System.Threading.Tasks;
using WooliesTest.Models;

namespace WooliesTest.Services
{
    public class TrolleyCalculator : ITrolleyCalculator
    {
        public async Task<decimal?> CalculateAsync(Trolley trolley)
        {
            decimal minTotalValue = CalculateTrolley(trolley.Products, trolley.Quantities);

            var currentQuantities = trolley.Quantities;


            var weightedSpecials = CalculateSpecialWeight(trolley.Products, trolley.Specials);

            var sortedWeightedSpecials = weightedSpecials.Where(s => s.SavingAmount > 0).OrderByDescending(s => s.SavingAmount);

            foreach (var weightedSpecial in sortedWeightedSpecials)
            {
                while (IsSpecialApplicable(weightedSpecial, currentQuantities))
                {
                    currentQuantities = ApplySpecial(weightedSpecial, currentQuantities);
                    minTotalValue -= weightedSpecial.SavingAmount;
                }
            }

            return minTotalValue;
        }

        static ProductQuantity[] ApplySpecial(WeightedSpecial special, ProductQuantity[] quantities)
        {
            foreach (var specialQuantity in special.Special.Quantities)
            {
                if (specialQuantity.Quantity > 0)
                {
                    var quantity = quantities.FirstOrDefault(p => p.Name == specialQuantity.Name);
                    {
                        quantity.Quantity -= specialQuantity.Quantity;
                    }
                }
            }

            return quantities;
        }

        static bool IsSpecialApplicable(WeightedSpecial special, ProductQuantity[] quantities)
        {
            foreach (var specialQuantity in special.Special.Quantities)
            {
                if (specialQuantity.Quantity > 0)
                {
                    if (!quantities.Any(p => p.Name == specialQuantity.Name && p.Quantity >= specialQuantity.Quantity))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        static WeightedSpecial[] CalculateSpecialWeight(Product[] products, Special[] specials)
        {
            return specials.Select(s => new WeightedSpecial()
            {
                Special = s,
                SavingAmount = CalculateTrolley(products, s.Quantities) - s.Total
            }).ToArray();
        }

        static decimal CalculateTrolley(Product[] products, ProductQuantity[] quantities)
        {
            decimal totalValue = 0;

            foreach (var quantity in quantities)
            {
                var product = products.FirstOrDefault(p => p.Name == quantity.Name);

                if (product != null)
                {
                    totalValue += product.Price * quantity.Quantity;
                }
            }

            return totalValue;
        }
    }
}
