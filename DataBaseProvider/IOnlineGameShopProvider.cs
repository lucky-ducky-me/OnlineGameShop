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

        public IEnumerable<Order> GetAllOrders();

        /// <summary>
        /// Получение всех оценок.
        /// </summary>
        /// <returns>Коллекция оценок.</returns>
        public IEnumerable<UserScore> GetAllUsersScore();

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

        public Order GetOrder(Guid id);

        /// <summary>
        /// Получение оценки.
        /// </summary>
        /// <param name="id">Id оценки.</param>
        /// <returns>Оценка.</returns>
        public UserScore GetUserScore(Guid id);

        public bool AddUser(User user);

        /// <summary>
        /// Добавление игры.
        /// </summary>
        /// <param name="game">Игра.</param>
        public void AddGame(Game game);

        public bool AddOrder(Order order);

        /// <summary>
        /// Добавление оценки.
        /// </summary>
        /// <param name="score">Оценка.</param>
        public void AddUserScore(UserScore score);

        public bool DeleteUser(Guid id);

        /// <summary>
        /// Удаление игры.
        /// </summary>
        /// <param name="id">Id игры.</param>
        public void DeleteGame(Guid id);

        public bool DeleteOrder(Guid id);

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

        public void UpdateGame(Game game);
    }
}
