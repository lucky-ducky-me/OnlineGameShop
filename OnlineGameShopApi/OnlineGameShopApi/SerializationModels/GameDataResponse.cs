namespace OnlineGameShopApi.SerializationModels
{
    /// <summary>
    /// Модель игры для ответа.
    /// </summary>
    public class GameDataResponse
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
        /// Жанр.
        /// </summary>
        public GenreDataResponse? Genre { get; set; }
    }
}
