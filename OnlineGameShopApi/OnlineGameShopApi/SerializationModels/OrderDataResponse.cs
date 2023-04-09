using System.ComponentModel.DataAnnotations;

namespace OnlineGameShopApi.SerializationModels
{
    /// <summary>
    /// Модель заказаза для ответов.
    /// </summary>
    public class OrderDataResponse
    {
        /// <summary>
        /// Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Время заказа.
        /// </summary>
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Id пользователя, сделавшего заказ.
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// Id заказанной игры.
        /// </summary>
        public Guid? GameId { get; set; }

        /// <summary>
        /// Игра.
        /// </summary>
        public GameDataResponse Game { get; set; }

        /// <summary>
        /// Пользователь.
        /// </summary>
        public UserData User { get; set; }
    }
}
