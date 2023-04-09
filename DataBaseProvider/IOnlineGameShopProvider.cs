using DataBaseProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider
{
    /// <summary>
    /// Провайдер к БД онлайн магазина игр.
    /// </summary>
    public interface IOnlineGameShopProvider
    {
        /// <summary>
        /// Получение всех пользователей.
        /// </summary>
        /// <returns>Коллекция пользователей.</returns>
        public IEnumerable<User> GetAllUsers();

        /// <summary>
        /// Получение всех жанров.
        /// </summary>
        /// <returns>Коллекция жанров.</returns>
        public IEnumerable<Genre> GetAllGenres();

        /// <summary>
        /// Получение всех игр.
        /// </summary>
        /// <returns>Коллекция игр.</returns>
        public IEnumerable<Game> GetAllGames();

        /// <summary>
        /// Получение всех заказов.
        /// </summary>
        /// <returns>Коллекция заказов.</returns>
        public IEnumerable<Order> GetAllOrders();

        /// <summary>
        /// Получение всех оценок.
        /// </summary>
        /// <returns>Коллекция оценок.</returns>
        public IEnumerable<UserScore> GetAllUsersScore();

        /// <summary>
        /// Получение пользователя.
        /// </summary>
        /// <param name="id">Id пользователя.</param>
        /// <returns>Пользователя.</returns>
        public User GetUser(Guid id);

        /// <summary>
        /// Получение жанра.
        /// </summary>
        /// <param name="id">Id жанра.</param>
        /// <returns>Жанр.</returns>
        public Genre GetGenre(Guid id);

        /// <summary>
        /// Получение игры.
        /// </summary>
        /// <param name="id">Id игры.</param>
        public Game GetGame(Guid id);

        /// <summary>
        /// Получение заказа по Id.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <returns>Заказ.</returns>
        public Order GetOrder(Guid id);

        /// <summary>
        /// Получение оценки.
        /// </summary>
        /// <param name="id">Id оценки.</param>
        /// <returns>Оценка.</returns>
        public UserScore GetUserScore(Guid id);

        /// <summary>
        /// Добавление пользователя.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        public void AddUser(User user);

        /// <summary>
        /// Добавление игры.
        /// </summary>
        /// <param name="game">Игра.</param>
        public void AddGame(Game game);

        /// <summary>
        /// Добавление заказа.
        /// </summary>
        /// <param name="order">Заказ.</param>
        public void AddOrder(Order order);

        /// <summary>
        /// Добавление оценки.
        /// </summary>
        /// <param name="score">Оценка.</param>
        public void AddUserScore(UserScore score);

        /// <summary>
        /// Удаление пользователя.
        /// </summary>
        /// <param name="id">Id пользователя.</param>
        public void DeleteUser(Guid id);

        /// <summary>
        /// Удаление игры.
        /// </summary>
        /// <param name="id">Id игры.</param>
        public void DeleteGame(Guid id);

        /// <summary>
        /// Удаление заказа.
        /// </summary>
        /// <param name="id">Id заказа.</param>
        public void DeleteOrder(Guid id);

        /// <summary>
        /// Удаление оценки.
        /// </summary>
        /// <param name="id">Id оценки.</param>
        public void DeleteUserScore(Guid id);

        /// <summary>
        /// Изменение данных оценки.
        /// </summary>
        /// <param name="score">Оценка.</param>
        public void UpdateUserScore(UserScore score);

        /// <summary>
        /// Изменение данных игры.
        /// </summary>
        /// <param name="game">Игра.</param>
        public void UpdateGame(Game game);

        /// <summary>
        /// Обновление пользователя.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        public void UpdateUser(User user);
    }
}
