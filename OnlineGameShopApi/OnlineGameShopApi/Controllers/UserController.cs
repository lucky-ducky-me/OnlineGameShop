using DataBaseProvider;
using DataBaseProvider.Models;
using Microsoft.AspNetCore.Mvc;

namespace OnlineGameShopApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration Configuration;

        private IOnlineGameShopProvider _onlineGameShopProvider;

        public UserController(IConfiguration configuration)
        {
            Configuration = configuration;
            _onlineGameShopProvider = new OnlineGameShopProvider(
                    Configuration.GetConnectionString("defaultConnection"));
        }

        [HttpGet("users")]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            try
            {
                return StatusCode(200, _onlineGameShopProvider.GetAllUsers().ToArray());
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpGet("users/{id}")]
        public ActionResult<User> GetUser(Guid id)
        {
            try
            {
                return StatusCode(200, _onlineGameShopProvider.GetUser(id));
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult<User> AddUser([FromBody] User user)
        {
            var result = _onlineGameShopProvider.AddUser(user);

            var uri = $"http://http://localhost:5142/api/users/{user.Id}";

            if (result)
            {
                return Created(uri, user);
            }
            else
            {
                return Problem();
            }
        }

        [HttpDelete("users")]
        public ActionResult DeleteUser(Guid id)
        {
            var result = _onlineGameShopProvider.DeleteUser(id);

            if (result)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
