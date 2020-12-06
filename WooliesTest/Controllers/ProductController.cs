using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WooliesTest.Models;
using WooliesTest.Repositories;
using WooliesTest.Services;

namespace WooliesTest.Controllers
{
    [ApiController]
    [Route("api/sort")]
    public class ProductController : ControllerBase
    {
        readonly IDataRepository repository;
        readonly IProductService productService;

        public ProductController(IDataRepository repository, IProductService productService)
        {
            this.repository = repository ?? throw new System.ArgumentNullException(nameof(repository));
            this.productService = productService ?? throw new System.ArgumentNullException(nameof(productService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(string sortOption)
        {
            if (!Enum.TryParse<ProductSortingOptions>(sortOption, out ProductSortingOptions sortingOption))
            {
                return BadRequest();
            }

            var products = await repository.GetProductsAsync();
            return Ok(await productService.SortAsync(products, sortingOption));
        }
    }
}
