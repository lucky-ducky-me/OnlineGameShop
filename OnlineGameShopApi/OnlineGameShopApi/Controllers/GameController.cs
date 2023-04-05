﻿using DataBaseProvider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net;
using DataBaseProvider.Models;
using OnlineGameShopApi.SerializationModels;

namespace OnlineGameShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        /// <summary>
        /// Конфигурация.
        /// </summary>
        private readonly IConfiguration Configuration;

        /// <summary>
        /// Провайдер БД.
        /// </summary>
        private IOnlineGameShopProvider _onlineGameShopProvider;

        public GameController(IConfiguration configuration)
        {
            Configuration = configuration;
            _onlineGameShopProvider = new OnlineGameShopProvider(
                Configuration.GetConnectionString("defaultConnection"));
        }

        /// <summary>
        /// Получение всех игр.
        /// </summary>
        /// <returns>Коллекция игр.</returns>
        [HttpGet("games")]
        public ActionResult<IEnumerable<GameDataResponse>> GetGames()
        {
            try
            {
                return StatusCode(200, _onlineGameShopProvider.GetAllGames()
                    .Select(game => TransformToGameResponse(game)).ToArray());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Получение игры по Id.
        /// </summary>
        /// <param name="id">Id игры.</param>
        /// <returns>Игра.</returns>
        [HttpGet("games/{id}")]
        public ActionResult<GameDataResponse> GetGame(Guid id)
        {
            try
            {
                return StatusCode(200, TransformToGameResponse(_onlineGameShopProvider.GetGame(id)));
            }
            catch (Exception ex) 
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Добавление игры.
        /// </summary>
        /// <param name="gameData">Данные для добавления.</param>
        /// <returns>Результат добавления.</returns>
        [HttpPost("games")]
        public ActionResult<Game> AddGame([FromBody] GameDataRequest gameData)
        {
            try
            {
                var game = new Game {
                    Id = Guid.NewGuid(),
                    Name = gameData.Name,
                    Cost = gameData.Cost,
                    GenreId = gameData.GenreId,
                };

                _onlineGameShopProvider.AddGame(game);

                var uri = $"http://http://localhost:5142/api/games/{game.Id}";

                return Created(uri, TransformToGameResponse(game));
               
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Обновление данных игры.
        /// </summary>
        /// <param name="gameDataRequest">Данные игры.</param>
        /// <returns>Результат обновления.</returns>
        [HttpPut("games")]
        public ActionResult<Game> UpdateGame([FromBody] GameDataRequest gameDataRequest)
        {
            try
            {
                var game = _onlineGameShopProvider.GetGame(gameDataRequest.Id);

                game.Cost = gameDataRequest.Cost;
                game.Name = gameDataRequest.Name;
                game.GenreId = gameDataRequest.GenreId;
                game.Genre = _onlineGameShopProvider.GetGenre(gameDataRequest.GenreId);

                _onlineGameShopProvider.UpdateGame(game);

                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Удаление игры.
        /// </summary>
        /// <param name="id">Id игры.</param>
        /// <returns>Статус удаления.</returns>
        [HttpDelete("games")]
        public ActionResult DeleteGame([FromQuery] Guid id)
        {
            try
            {
                _onlineGameShopProvider.DeleteGame(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            
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
                    .Select(genre => TransformToGenreResponse(genre)).ToArray());
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
                return StatusCode(200, TransformToGenreResponse(_onlineGameShopProvider.GetGenre(id)));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Преобразование жанра из сущности БД в модель для ответа.
        /// </summary>
        /// <param name="genre">Сущность БД.</param>
        /// <returns>Модель для ответа.</returns>
        private GenreDataResponse TransformToGenreResponse(Genre genre)
        {
            if (genre == null)
            {
                return null;
            }

            return new GenreDataResponse { Id = genre.Id, GenreName = genre.GenreName };
        }

        /// <summary>
        ///  Преобразование игры из сущности БД в модель для ответа.
        /// </summary>
        /// <param name="game">Сущность БД.</param>
        /// <returns>Модель для ответа.</returns>
        private GameDataResponse TransformToGameResponse(Game game)
        {
            if (game == null)
            {
                return null;
            }

            var genreResponse = TransformToGenreResponse(_onlineGameShopProvider.GetGenre((Guid) game.GenreId));

            return new GameDataResponse { 
                Id = game.Id
                , Genre = genreResponse
                , Cost = game.Cost
                , Name = game.Name };
        }
    }
}
