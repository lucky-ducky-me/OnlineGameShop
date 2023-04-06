namespace OnlineGameShopApi.SerializationModels
{
    /// <summary>
    /// Модель оценки игры пользователем для ответов контроллеров.
    /// </summary>
    public class UserScoreDataResponse
    {
        /// <summary>
        /// Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Оценка.
        /// </summary>
        public short Score { get; set; }

        /// <summary>
        /// Id пользователя, сделавшего оценку.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Id оценённой игры.
        /// </summary>
        public Guid GameId { get; set; }

        /// <summary>
        /// Сделавший оценку пользователь.
        /// </summary>
        public UserData User { get; set; }

        /// <summary>
        /// Оценённая игра.
        /// </summary>
        public GameDataResponse Game { get; set; }
    }
}
