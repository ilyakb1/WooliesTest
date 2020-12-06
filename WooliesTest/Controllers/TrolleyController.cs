using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;
using WooliesTest.Models;
using WooliesTest.Services;

namespace WooliesTest.Controllers
{
    [ApiController]
    [Route("api/trolleyTotal")]
    public class TrolleyController : ControllerBase
    {
        readonly ITrolleyCalculator trolleyCalculator;
        private readonly ILogger<TrolleyController> logger;

        public TrolleyController(ITrolleyCalculator trolleyCalculator, ILogger<TrolleyController> logger)
        {
            this.trolleyCalculator = trolleyCalculator ?? throw new System.ArgumentNullException(nameof(trolleyCalculator));
            this.logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public async Task<IActionResult> CalculateAsync(Trolley trolley)
        {
            var input = JsonConvert.SerializeObject(trolley);
            logger.LogInformation(input);

            if (trolley == null)
            {
                return BadRequest();
            }

            return Ok(await trolleyCalculator.CalculateAsync(trolley));
        }
    }
}
