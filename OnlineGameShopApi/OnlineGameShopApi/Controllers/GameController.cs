using DataBaseProvider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

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
        [HttpGet]
        public ActionResult<IEnumerable<DataBaseProvider.Models.Game>> Games()
        { 
            try
            {
                return _onlineGameShopProvider.GetAllGames().ToArray();
            }
            catch (Exception ex)
            {
                return NoContent();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<DataBaseProvider.Models.Game> Game(Guid id)
        {
            try
            {
                return _onlineGameShopProvider.GetGame(id);
            }
            catch (Exception ex) 
            {
                return NoContent();
            }
        }
    }
}
