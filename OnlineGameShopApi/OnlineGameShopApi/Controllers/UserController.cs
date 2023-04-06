using DataBaseProvider;
using DataBaseProvider.Models;
using Microsoft.AspNetCore.Mvc;
using OnlineGameShopApi.SerializationModels;

namespace OnlineGameShopApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Конфигурация.
        /// </summary>
        private readonly IConfiguration Configuration;

        /// <summary>
        /// Провайдер БД.
        /// </summary>
        private IOnlineGameShopProvider _onlineGameShopProvider;

        public UserController(IConfiguration configuration)
        {
            Configuration = configuration;
            _onlineGameShopProvider = new OnlineGameShopProvider(
                    Configuration.GetConnectionString("defaultConnection"));
        }

        /// <summary>
        /// Получение всех пользователей.
        /// </summary>
        /// <returns>Коллекция пользователей.</returns>
        [HttpGet("users")]
        public ActionResult<IEnumerable<UserData>> GetUsers()
        {
            try
            {
                return StatusCode(200, _onlineGameShopProvider.GetAllUsers()
                    .Select(user => Transform.TransformToUserData(user)).ToArray());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Получение пользователя по Id.
        /// </summary>
        /// <param name="id">Id пользователя.</param>
        /// <returns>Пользователь.</returns>
        [HttpGet("users/{id}")]
        public ActionResult<UserData> GetUser(Guid id)
        {
            try
            {
                return StatusCode(200, Transform.TransformToUserData(_onlineGameShopProvider.GetUser(id)));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Добавление пользователя.
        /// </summary>
        /// <param name="userData">Модель пользователя.</param>
        /// <returns>Результат добавления.</returns>
        [HttpPost("users")]
        public ActionResult<UserData> AddUser([FromBody] UserData userData)
        {
            try
            {
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Name = userData.Name,
                    Login = userData.Login,
                    Password = userData.Password,
                    Phone = userData.Phone,
                };

                _onlineGameShopProvider.AddUser(user);

                var uri = $"http://http://localhost:5142/api/users/{user.Id}";

                return Created(uri, Transform.TransformToUserData(user));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Обновление данных пользователя.
        /// </summary>
        /// <param name="userData">Модель пользователя.</param>
        /// <returns>Результат обновления.</returns>
        [HttpPut("users")]
        public ActionResult<UserData> UpdateUser([FromBody] UserData userData)
        {
            try
            {
                var user = _onlineGameShopProvider.GetUser(userData.Id);

                if (user == null)
                {
                    throw new ArgumentException("Указанного пользователя не существует.", nameof(userData));
                }

                user.Name = userData.Name;
                user.Login = userData.Login;
                user.Phone = userData.Phone;

                _onlineGameShopProvider.UpdateUser(user);

                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Удаление пользователя.
        /// </summary>
        /// <param name="id">Id пользователя.</param>
        /// <returns>Статус удаления.</returns>
        [HttpDelete("users")]
        public ActionResult DeleteUser(Guid id)
        {
            try
            {
                _onlineGameShopProvider.DeleteUser(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
