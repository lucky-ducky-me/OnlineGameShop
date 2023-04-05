namespace OnlineGameShopApi.SerializationModels
{
    /// <summary>
    /// Модель оценки игры пользователем для запросов к контроллерам.
    /// </summary>
    public class UserScoreDataRequest
    {
        public short Score { get; set; }

        public Guid UserId { get; set; }

        public Guid GameId { get; set; }
    }
}
