namespace OnlineGameShopApi.SerializationModels
{
    /// <summary>
    /// Модель заказаза для запросов.
    /// </summary>
    public class OrderDataRequest
    {
        /// <summary>
        /// Время заказа.
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Id пользователя, сделавшего заказ.
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// Id заказанной игры.
        /// </summary>
        public Guid? GameId { get; set; }
    }
}
