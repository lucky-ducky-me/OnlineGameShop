using DataBaseProvider;
using Microsoft.AspNetCore.Mvc;
using OnlineGameShopApi.SerializationModels;

namespace OnlineGameShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        /// <summary>
        /// Конфигурация.
        /// </summary>
        private readonly IConfiguration Configuration;

        /// <summary>
        /// Провайдер БД.
        /// </summary>
        private IOnlineGameShopProvider _onlineGameShopProvider;

        public GenreController(IConfiguration configuration)
        {
            Configuration = configuration;
            _onlineGameShopProvider = new OnlineGameShopProvider(
                Configuration.GetConnectionString("defaultConnection"));
        }

        /// <summary>
        /// Получение жанров игр.
        /// </summary>
        /// <returns>Коллекция жанров.</returns>
        [HttpGet("genres")]
        public ActionResult<IEnumerable<GenreDataResponse>> GetGenres()
        {
            try
            {
                return StatusCode(200, _onlineGameShopProvider.GetAllGenres()
                    .Select(genre => Transform.TransformToGenreResponse(genre)).ToArray());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Получение жанра по Id.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <returns>Жанр.</returns>
        [HttpGet("genres/{id}")]
        public ActionResult<GenreDataResponse> GetGenre(Guid id)
        {
            try
            {
                return StatusCode(200, Transform.TransformToGenreResponse(_onlineGameShopProvider.GetGenre(id)));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
