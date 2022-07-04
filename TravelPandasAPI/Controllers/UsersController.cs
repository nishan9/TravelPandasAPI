using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelPandasAPI.Data;
using TravelPandasAPI.Data.Tables;


namespace TravelPandasAPI
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private DataContext context;

        public UsersController(DataContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            user.Auth0_Id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return new OkObjectResult(user); 
        }

        [HttpGet("Capacity/{driverId}")]
        public async Task<IActionResult> GetCapacity(string driverId)
        {
            var driver = context.Users.FirstOrDefault(user => user.Auth0_Id == driverId);
            return new OkObjectResult(driver.Capacity); 
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(string userId)
        {
            var user = context.Users.FirstOrDefault(user => user.Auth0_Id == userId);
            return new OkObjectResult(user);
        }

        [HttpGet("isRegistered")]
        public async Task<IActionResult> IsRegistered()
        {
            var AuthId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var userExists = context.Users.FirstOrDefault(user => user.Auth0_Id == AuthId);
            if (userExists == null)
            {
                return new OkObjectResult(false);
            }

            return new OkObjectResult(true);
        }


    }
}

