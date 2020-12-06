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
            var products = new Product[]
               {
                   new Product()
                   {
                       Name = "Product 1",
                       Price = 10
                   },
                    new Product()
                   {
                       Name = "Product 2",
                       Price = 20

                   },

                   new Product()
                   {
                       Name = "Product 3",
                       Price = 30

                   },
                   new Product()
                   {
                       Name = "Product 4",
                       Price = 40

                   },
                   new Product()
                   {
                       Name = "Product 5",
                       Price = 50
                   }
               };

            var quantities = new ProductQuantity[]
               {
                   new ProductQuantity()
                   {
                       Name = "Product 1",
                       Quantity = 5
                   },
                    new ProductQuantity()
                   {
                       Name = "Product 2",
                       Quantity = 7

                   },

                   new ProductQuantity()
                   {
                       Name = "Product 3",
                       Quantity = 4

                   },

                   new ProductQuantity()
                   {
                       Name = "Product 5",
                       Quantity = 6

                   }
            };

            var specials = new Special[]
               {
                   new Special()
                   {
                       Quantities = new ProductQuantity[]{
                           new ProductQuantity()
                           {
                               Name = "Product 1",
                               Quantity = 2
                           },
                           new ProductQuantity()
                           {
                               Name = "Product 3",
                               Quantity = 3
                           }
                       },
                       Total = 90
                   },
                   new Special()
                   {
                       Quantities = new ProductQuantity[]{
                           new ProductQuantity()
                           {
                               Name = "Product 2",
                               Quantity = 3
                           },
                           new ProductQuantity()
                           {
                               Name = "Product 5",
                               Quantity = 5
                           }
                       },
                       Total = 150
                   },

                   new Special()
                   {
                       Quantities = new ProductQuantity[]{
                           new ProductQuantity()
                           {
                               Name = "Product 2",
                               Quantity = 3
                           },
                           new ProductQuantity()
                           {
                               Name = "Product 4",
                               Quantity = 4
                           },
                           new ProductQuantity()
                           {
                               Name = "Product 5",
                               Quantity = 5
                           }
                       },
                       Total = 10
                   },

                   new Special()
                   {
                       Quantities = new ProductQuantity[]{
                           new ProductQuantity()
                           {
                               Name = "Product 2",
                               Quantity = 2
                           },
                           new ProductQuantity()
                           {
                               Name = "Product 3",
                               Quantity = 2
                           },
                           new ProductQuantity()
                           {
                               Name = "Product 5",
                               Quantity = 2
                           }
                       },
                       Total = 20
                   }
            };

            var trolley = new Trolley()
            {
                Products = products,
                Quantities = quantities,
                Specials = specials
            };

            var calculator = new TrolleyCalculator();
            var totalValue = await calculator.CalculateAsync(trolley);

            Assert.Equal(250, totalValue);
        }

        [Fact]
        public async Task Calculate_Trolley_MinTotalAmount_2()
        {
            var products = new Product[]
               {
                   new Product()
                   {
                       Name = "1",
                       Price = 2
                   },
                    new Product()
                   {
                       Name = "2",
                       Price = 5

                   }
               };

            var quantities = new ProductQuantity[]
               {
                   new ProductQuantity()
                   {
                       Name = "1",
                       Quantity = 3
                   },
                    new ProductQuantity()
                   {
                       Name = "2",
                       Quantity = 2

                   }
            };

            var specials = new Special[]
               {
                   new Special()
                   {
                       Quantities = new ProductQuantity[]{
                           new ProductQuantity()
                           {
                               Name = "1",
                               Quantity = 3
                           },
                           new ProductQuantity()
                           {
                               Name = "2",
                               Quantity = 0
                           }
                       },
                       Total = 5
                   },
                   new Special()
                   {
                       Quantities = new ProductQuantity[]{
                           new ProductQuantity()
                           {
                               Name = "1",
                               Quantity = 1
                           },
                           new ProductQuantity()
                           {
                               Name = "2",
                               Quantity = 2
                           }
                       },
                       Total = 10
                   }
                };

            var trolley = new Trolley()
            {
                Products = products,
                Quantities = quantities,
                Specials = specials
            };

            var calculator = new TrolleyCalculator();
            var totalValue = await calculator.CalculateAsync(trolley);

            Assert.Equal(14, totalValue);
        }
    }
}

