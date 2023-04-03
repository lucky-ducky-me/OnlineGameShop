﻿using DataBaseProvider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net;
using DataBaseProvider.Models;

namespace OnlineGameShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IConfiguration Configuration;

        private IOnlineGameShopProvider _onlineGameShopProvider;

        public GameController(IConfiguration configuration)
        {
            Configuration = configuration;
            _onlineGameShopProvider = new OnlineGameShopProvider(
                Configuration.GetConnectionString("defaultConnection"));
        }

        [HttpGet("games")]
        public ActionResult<IEnumerable<Game>> GetGames()
        {
            try
            {
                return StatusCode(200, _onlineGameShopProvider.GetAllGames().ToArray());
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpGet("games/{id}")]
        public ActionResult<Game> GetGame(Guid id)
        {
            try
            {
                return StatusCode(200, _onlineGameShopProvider.GetGame(id));
            }
            catch (Exception ex) 
            {
                return NotFound();
            }
        }

        [HttpPost("games")]
        public ActionResult<Game> AddGame([FromBody] Game game) 
        {
            var result = _onlineGameShopProvider.AddGame(game);

            var uri = $"http://http://localhost:5142/api/games/{game.Id}";

            if (result)
            {
                return Created(uri, game);
            }
            else
            {
                return Problem();
            }
        }

        [HttpDelete("games")]
        public ActionResult<Game> DeleteGame(Guid id)
        {
            var result = _onlineGameShopProvider.DeleteGame(id);

            if (result)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("genres")]
        public ActionResult<IEnumerable<Genre>> GetGenres()
        {
            try
            {
                return StatusCode(200, _onlineGameShopProvider.GetAllGenres().ToArray());
            }
            catch (Exception ex) 
            {
                return NotFound();
            }
        }

        [HttpGet("genres/{id}")]
        public ActionResult<Genre> GetGenre(Guid id)
        {
            try
            {
                return StatusCode(200, _onlineGameShopProvider.GetGenre(id));
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
