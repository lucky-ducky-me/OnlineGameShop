namespace OnlineGameShopApi.SerializationModels
{
    /// <summary>
    /// Модель игры для запроса.
    /// </summary>
    public class GameDataRequest
    {
        /// <summary>
        /// Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Стоимость.
        /// </summary>
        public int Cost { get; set; }

        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Id жанра.
        /// </summary>
        public Guid GenreId { get; set; }
    }
}
