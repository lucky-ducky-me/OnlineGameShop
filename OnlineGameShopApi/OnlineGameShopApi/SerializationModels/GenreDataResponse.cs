namespace OnlineGameShopApi.SerializationModels
{
    /// <summary>
    /// Модель жанра для ответа.
    /// </summary>
    public class GenreDataResponse
    {
        /// <summary>
        /// Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название жанра.
        /// </summary>
        public string GenreName { get; set; } = null!;
    }
}
