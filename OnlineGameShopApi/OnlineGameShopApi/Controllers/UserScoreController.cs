using DataBaseProvider;
using DataBaseProvider.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineGameShopApi.SerializationModels;

namespace OnlineGameShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserScoreController : ControllerBase
    {
        /// <summary>
        /// Конфигурация.
        /// </summary>
        private readonly IConfiguration Configuration;

        /// <summary>
        /// Провайдер БД.
        /// </summary>
        private IOnlineGameShopProvider _onlineGameShopProvider;

        public UserScoreController(IConfiguration configuration)
        {
            Configuration = configuration;
            _onlineGameShopProvider = new OnlineGameShopProvider(
                    Configuration.GetConnectionString("defaultConnection"));
        }

        /// <summary>
        /// Получение всех оценок пользователей.
        /// </summary>
        /// <returns>Коллекция оценок.</returns>
        [HttpGet("scores")]
        public ActionResult<IEnumerable<UserScoreDataResponse>> GetScores()
        {
            try
            {
                return StatusCode(200, _onlineGameShopProvider.GetAllUsersScore()
                    .Select(userScore => {
                        var game = _onlineGameShopProvider.GetGame((Guid)userScore.GameId);
                        game.Genre = _onlineGameShopProvider.GetGenre((Guid)game.GenreId);

                        var user = _onlineGameShopProvider.GetUser((Guid)userScore.UserId);

                        userScore.User = user;
                        userScore.Game = game;

                        return Transform.TransformToUserScoreDataResponse(userScore);
                        })
                    .ToArray());
            }
            catch (Exception ex) 
            {
                return NotFound(ex.Message);
            }
        }
  
        /// <summary>
        /// Получение оценки пользователя по Id.
        /// </summary>
        /// <param name="id">Id пользователя.</param>
        /// <returns>Оценка.</returns>
        [HttpGet("scores/{id}")]
        public ActionResult<UserScoreDataResponse> GetScore(Guid id)
        {
            try
            {
                var userScore = _onlineGameShopProvider.GetUserScore(id);

                var game = _onlineGameShopProvider.GetGame((Guid)userScore.GameId);
                game.Genre = _onlineGameShopProvider.GetGenre((Guid)game.GenreId);

                var user = _onlineGameShopProvider.GetUser((Guid)userScore.UserId);

                userScore.User = user;
                userScore.Game = game;

                return StatusCode(200, Transform.TransformToUserScoreDataResponse(userScore));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Добавление оценки игры.
        /// </summary>
        /// <param name="userScoreData">Модель оценки для запроса.</param>
        /// <returns>Результат добавления.</returns>
        [HttpPost("scores")]
        public ActionResult<UserScoreDataResponse> AddScore([FromBody] UserScoreDataRequest userScoreData)
        {
            try
            {
                var userScore = new UserScore
                {
                    Id = Guid.NewGuid()
                    , GameId = userScoreData.GameId
                    , UserId = userScoreData.UserId
                    , Score = userScoreData.Score
                };

                _onlineGameShopProvider.AddUserScore(userScore);

                var uri = $"http://http://localhost:5142/api/scores/{userScore.Id}";

                var game = _onlineGameShopProvider.GetGame((Guid)userScore.GameId);
                game.Genre = _onlineGameShopProvider.GetGenre((Guid)game.GenreId);

                var user = _onlineGameShopProvider.GetUser((Guid)userScore.UserId);

                userScore.User = user;
                userScore.Game = game;

                var userScoreDataResponse = Transform.TransformToUserScoreDataResponse(userScore);

                return Created(uri, userScoreDataResponse);
            }
            catch (Exception ex) 
            {
                return Problem(ex.ToString());
            }
        }

        /// <summary>
        /// Обновление данных оценки.
        /// </summary>
        /// <param name="userScoreData">Модель оценки для запроса.</param>
        /// <returns>Результать обновления.</returns>
        [HttpPut("scores")]
        public ActionResult UpdateScore([FromQuery] Guid id, [FromQuery] short score)
        {
            try
            {
                var userScore = _onlineGameShopProvider.GetUserScore(id);

                userScore.Score = score;

                _onlineGameShopProvider.UpdateUserScore(userScore);

                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.ToString());
            }
        }

        /// <summary>
        /// Удаление игры по Id.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <returns>Статус удаления.</returns>
        [HttpDelete("scores")]
        public ActionResult DeleteScore(Guid id)
        {
            try
            {
                _onlineGameShopProvider.DeleteUserScore(id);

                return NoContent();
            }
            catch (Exception ex) 
            {   
                return NotFound(ex.Message); 
            }
        }

        
    }
}
