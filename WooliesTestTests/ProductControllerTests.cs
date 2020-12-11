using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WooliesTest;
using WooliesTest.Controllers;
using WooliesTest.Models;
using WooliesTest.Repositories;
using WooliesTest.Services;
using Xunit;

namespace WooliesTestTests.TestFiles
{
    public class ProductControllerTests
    {
        readonly ProductController Controller;

        public ProductControllerTests()
        {

            var product1 = new Product()
            {
                Name = "Product 3",
                Price = 40
            };
            var product2 = new Product()
            {
                Name = "Product 2",
                Price = 60

            };

            var product3 = new Product()
            {
                Name = "Product 1",
                Price = 100

            };

            var products = new List<Product> { product1, product2, product3 };

            var shopperHistories = new List<ShopperHistory>
               {
                   new ShopperHistory()
                   {
                       CustomerId = 1,
                       Products = new Product[]{ product1, product2, product3 }
                   },

                   new ShopperHistory()
                   {
                       CustomerId = 2,
                       Products = new Product[]{ product2, product3 }
                   },

                   new ShopperHistory()
                   {
                       CustomerId = 3,
                       Products = new Product[]{ product2 }
                   }
            };


            var repository = new Mock<IDataRepository>();
            repository.Setup(r => r.GetProductsAsync()).Returns(Task.FromResult<IEnumerable<Product>>(products));
            repository.Setup(r => r.GetShopperHistoriesAsync()).Returns(Task.FromResult<IEnumerable<ShopperHistory>>(shopperHistories));

            var shopperHistoryService = new ShopperHistoryService(repository.Object);
            var recommendedProductsService = new RecommendedProductsService(shopperHistoryService);
            var productService = new ProductService(recommendedProductsService);

            Controller = new ProductController(repository.Object, productService);
        }

        [Fact]
        public async Task GetProducts_SortOptionIsLow_SortedProducts()
        {
            // Act
            var okResult = await Controller.GetAsync("Low") as OkObjectResult;

            // Assert
            var items = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);
            Assert.Equal("Product 3", items.First().Name);
        }

        [Fact]
        public async Task GetProducts_SortOptionIsHigh_SortedProducts()
        {
            // Act
            var okResult = await Controller.GetAsync("High") as OkObjectResult;

            // Assert
            var items = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);
            Assert.Equal("Product 1", items.First().Name);
        }

        [Fact]
        public async Task GetProducts_SortOptionIsAscending_SortedProducts()
        {
            // Act
            var okResult = await Controller.GetAsync("Ascending") as OkObjectResult;

            // Assert
            var items = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);
            Assert.Equal("Product 1", items.First().Name);
        }

        [Fact]
        public async Task GetProducts_SortOptionIsDescending_SortedProducts()
        {
            // Act
            var okResult = await Controller.GetAsync("Descending") as OkObjectResult;

            // Assert
            var items = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);
            Assert.Equal("Product 3", items.First().Name);
        }

        [Fact]
        public async Task GetProducts_SortOptionIsRecommended_SortedProducts()
        {
            // Act
            var okResult = await Controller.GetAsync("Recommended") as OkObjectResult;

            // Assert
            var items = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);
            Assert.Equal("Product 2", items.First().Name);
        }

        [Fact]
        public async Task GetProducts_SortOptionIsUnknow_BadRequest()
        {
            // Act
            var result = await Controller.GetAsync("Unknown");

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task GetProducts_ShopHistoryApiError_InternalServcerError()
        {
            var product1 = new Product()
            {
                Name = "Product 3",
                Price = 40
            };

            var products = new List<Product> { product1 };

            var shopperHistories = new List<ShopperHistory>
               {
                   new ShopperHistory()
                   {
                       CustomerId = 1,
                       Products = new Product[]{ product1 }
                   },
            };

            var repository = new Mock<IDataRepository>();
            repository.Setup(r => r.GetProductsAsync()).Returns(Task.FromResult<IEnumerable<Product>>(products));
            repository.Setup(r => r.GetShopperHistoriesAsync()).Throws(new ApiException("Error during call to ShopperHistories API"));
            var shopperHistoryService = new ShopperHistoryService(repository.Object);
            var recommendedProductsService = new RecommendedProductsService(shopperHistoryService);
            var productService = new ProductService(recommendedProductsService);
            var controller = new ProductController(repository.Object, productService);

            var exception = await Assert.ThrowsAsync<ApiException>(async () => await controller.GetAsync("Recommended"));
            Assert.Equal("Error during call to ShopperHistories API", exception.Message);
        }
    }
}
