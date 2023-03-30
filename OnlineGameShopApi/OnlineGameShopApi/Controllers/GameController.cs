using DataBaseProvider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net;

namespace OnlineGameShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        //todo вынести строку в конфиг файл
        private IOnlineGameShopProvider _onlineGameShopProvider
            = new OnlineGameShopProvider("Server=DESKTOP-N04FOJI;Database=OnlineGameShop;Trusted_Connection=True;TrustServerCertificate=true");

        //todo изменинть на класс модели, а не на класс сущности бд
        [HttpGet("games")]
        public ActionResult<IEnumerable<DataBaseProvider.Models.Game>> Games()
        {
            try
            {
                return _onlineGameShopProvider.GetAllGames().ToArray();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpGet("games/{id}")]
        public ActionResult<DataBaseProvider.Models.Game> Game(Guid id)
        {
            try
            {
                return _onlineGameShopProvider.GetGame(id);
            }
            catch (Exception ex) 
            {
                return NotFound();
            }
        }

        [HttpPost("games")]
        public ActionResult<DataBaseProvider.Models.Game> AddGame(DataBaseProvider.Models.Game game) 
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
        public ActionResult<DataBaseProvider.Models.Game> DeleteGame(DataBaseProvider.Models.Game game)
        {
            var result = _onlineGameShopProvider.DeleteGame(game);

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
