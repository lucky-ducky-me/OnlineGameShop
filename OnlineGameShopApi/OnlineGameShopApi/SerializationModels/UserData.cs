namespace OnlineGameShopApi.SerializationModels
{
    /// <summary>
    /// Модель пользователя для запросов и ответов.
    /// </summary>
    public class UserData
    {
        /// <summary>
        /// Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Логин.
        /// </summary>
        public string Login { get; set; } = null!;

        /// <summary>
        /// Телефон.
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        public string Password { get; set; } = null!;
    }
}
