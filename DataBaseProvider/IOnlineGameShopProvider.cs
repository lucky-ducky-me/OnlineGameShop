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

        public IEnumerable<Genre> GetAllGenres();

        public IEnumerable<Game> GetAllGames();

        public IEnumerable<Order> GetAllOrders();

        /// <summary>
        /// Получение всех оценок.
        /// </summary>
        /// <returns>Коллекция оценок.</returns>
        public IEnumerable<UserScore> GetAllUsersScore();

        public User GetUser(Guid id);

        public Genre GetGenre(Guid id);

        public Game GetGame(Guid id);   

        public Order GetOrder(Guid id);

        /// <summary>
        /// Получение оценки.
        /// </summary>
        /// <param name="id">Id оценки.</param>
        /// <returns>Оценка.</returns>
        public UserScore GetUserScore(Guid id);

        public bool AddUser(User user);

        public bool AddGame(Game game);

        public bool AddOrder(Order order);

        /// <summary>
        /// Добавление оценки.
        /// </summary>
        /// <param name="score">Оценка.</param>
        public void AddUserScore(UserScore score);

        public bool DeleteUser(Guid id);

        public bool DeleteGame(Guid id);

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
    }
}
