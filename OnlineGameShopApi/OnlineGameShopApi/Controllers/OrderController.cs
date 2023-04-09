using DataBaseProvider;
using DataBaseProvider.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineGameShopApi.SerializationModels;

namespace OnlineGameShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        /// <summary>
        /// Конфигурация.
        /// </summary>
        private readonly IConfiguration Configuration;

        /// <summary>
        /// Провайдер БД.
        /// </summary>
        private IOnlineGameShopProvider _onlineGameShopProvider;

        public OrderController(IConfiguration configuration)
        {
            Configuration = configuration;
            _onlineGameShopProvider = new OnlineGameShopProvider(
                    Configuration.GetConnectionString("defaultConnection"));
        }

        /// <summary>
        /// Получение всех заказов.
        /// </summary>
        /// <returns>Коллекция заказов.</returns>
        [HttpGet("orders")]
        public ActionResult<IEnumerable<OrderDataResponse>> GetOrders()
        {
            try
            {
                return StatusCode(200, _onlineGameShopProvider.GetAllOrders().Select(order => {
                    var game = _onlineGameShopProvider.GetGame((Guid)order.GameId);
                    game.Genre = _onlineGameShopProvider.GetGenre((Guid)game.GenreId);

                    var user = _onlineGameShopProvider.GetUser((Guid)order.UserId);

                    order.User = user;
                    order.Game = game;

                    return Transform.TransformToOrderDataResponse(order);
                }).ToArray());
            }
            catch (Exception ex) 
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Получение заказа.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("orders/{id}")]
        public ActionResult<OrderDataResponse> GetOrder(Guid id)
        {
            try
            {
                var order = _onlineGameShopProvider.GetOrder(id);

                var game = _onlineGameShopProvider.GetGame((Guid)order.GameId);
                game.Genre = _onlineGameShopProvider.GetGenre((Guid)game.GenreId);

                var user = _onlineGameShopProvider.GetUser((Guid)order.UserId);

                order.User = user;
                order.Game = game;

                return StatusCode(200,  Transform.TransformToOrderDataResponse(order));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("orders")]
        public ActionResult<OrderDataResponse> AddOrder([FromBody] OrderDataRequest orderData)
        {
            try
            {
                var order = new Order
                {
                    Id = Guid.NewGuid(),
                    GameId = orderData.GameId,
                    UserId = orderData.UserId,
                    OrderDate = orderData.OrderDate,
                };

                _onlineGameShopProvider.AddOrder(order);

                var uri = $"http://http://localhost:5142/api/orders/{order.Id}";

                var game = _onlineGameShopProvider.GetGame((Guid)order.GameId);
                game.Genre = _onlineGameShopProvider.GetGenre((Guid)game.GenreId);

                var user = _onlineGameShopProvider.GetUser((Guid)order.UserId);

                order.User = user;
                order.Game = game;

                var orderDataResponse = Transform.TransformToOrderDataResponse(order);

                return Created(uri, orderDataResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.ToString());
            }
        }

        /// <summary>
        /// Удаление заказа.
        /// </summary>
        /// <param name="id">Id заказа.</param>
        /// <returns></returns>
        [HttpDelete("orders/{id}")]
        public ActionResult DeleteOrder(Guid id)
        {
            try
            {
                _onlineGameShopProvider.DeleteOrder(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
