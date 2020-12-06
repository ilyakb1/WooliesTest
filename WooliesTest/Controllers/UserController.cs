using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WooliesTest.Models;

namespace WooliesTest.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public UserController(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new System.ArgumentNullException(nameof(configuration));
        }

        [HttpGet]
        public ActionResult<User> Get()
        {
            var userName = configuration["Name"];
            var token = configuration["Token"];

            var user = new User()
            {
                Name = userName,
                Token = token
            };

            return Ok(user);
        }
    }
}
