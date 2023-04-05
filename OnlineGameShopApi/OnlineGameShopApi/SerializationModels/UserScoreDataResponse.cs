namespace OnlineGameShopApi.SerializationModels
{
    /// <summary>
    /// Модель оценки игры пользователем для ответов контроллеров.
    /// </summary>
    public class UserScoreDataResponse
    {
        public Guid Id { get; set; }

        public short Score { get; set; }

        public Guid UserId { get; set; }

        public Guid GameId { get; set; }

        public string? UserName { get; set; }

        public string? GameName { get; set; }
    }
}
