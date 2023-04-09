using DataBaseProvider.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace DataBaseProvider
{
    /// <summary>
    /// Провайдер к БД онлайн магазина игр.
    /// </summary>
    public class OnlineGameShopProvider : IOnlineGameShopProvider
    {
        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        private OnlineGameShopContext _dbContext;

        /// <summary>
        /// Строка подключения к БД.
        /// </summary>
        internal string ConnectionString { get; set; }

        /// <summary>
        /// Создание провайдера.
        /// </summary>
        /// <param name="connectionString">Строка подключение к БД.</param>
        /// <exception cref="ArgumentNullException">Пустая строка.</exception>
        public OnlineGameShopProvider(string connectionString) 
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException("Connection string should be initialize.", nameof(connectionString));
            }

            _dbContext = new OnlineGameShopContext(connectionString);
        }

        /// <summary>
        /// Получение всех игр.
        /// </summary>
        /// <returns>Коллекция игр.</returns>
        public IEnumerable<Game> GetAllGames()
        {
            return _dbContext.Games.ToList();
        }

        /// <summary>
        /// Получение всех жанров.
        /// </summary>
        /// <returns>Коллекция жанров.</returns>
        public IEnumerable<Genre> GetAllGenres()
        {
            return _dbContext.Genres.ToList();
        }

        /// <summary>
        /// Получение всех заказов.
        /// </summary>
        /// <returns>Коллекция заказов.</returns>
        public IEnumerable<Order> GetAllOrders()
        {
            return _dbContext.Orders;
        }

        /// <summary>
        /// Получение всех пользователей.
        /// </summary>
        /// <returns>Коллекция пользователей.</returns>
        public IEnumerable<User> GetAllUsers()
        {
            return _dbContext.Users.ToList();
        }

        /// <summary>
        /// Получение всех оценок.
        /// </summary>
        /// <returns>Коллекция оценок.</returns>
        public IEnumerable<UserScore> GetAllUsersScore()
        {
            return _dbContext.UserScores.ToList();
        }

        /// <summary>
        /// Получение пользователя.
        /// </summary>
        /// <param name="id">Id пользователя.</param>
        /// <returns>Пользователя.</returns>
        /// <exception cref="ArgumentException"></exception>
        public User GetUser(Guid id)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                throw new ArgumentException($"Пользователя с Id '{id}' не существует.", nameof(id));
            }

            return user;
        }

        /// <summary>
        /// Получение жанра.
        /// </summary>
        /// <param name="id">Id жанра.</param>
        /// <returns>Жанр.</returns>
        /// <exception cref="ArgumentException"></exception>
        public Genre GetGenre(Guid id)
        {
            var genre = _dbContext.Genres.FirstOrDefault(x => x.Id == id);

            if (genre == null)
            {
                throw new ArgumentException($"Жанра с Id '{id}' не существует.", nameof(id));
            }
            
            return genre;
        }

        /// <summary>
        /// Получение игры.
        /// </summary>
        /// <param name="id">Id игры.</param>
        /// <returns>Игра.</returns>
        /// <exception cref="Exception"></exception>
        public Game GetGame(Guid id)
        {
            var game = _dbContext.Games.FirstOrDefault(x => x.Id == id);

            if (game == null)
            {
                 throw new ArgumentException($"Игра с Id '{id}' не существует.", nameof(id));
            }

            return game;
        }

        /// <summary>
        /// Получение заказа по Id.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <returns>Заказ.</returns>
        /// <exception cref="ArgumentException"></exception>
        public Order GetOrder(Guid id)
        {
            var order = _dbContext.Orders.FirstOrDefault(x => x.Id == id);

            if (order == null)
            {
                throw new ArgumentException($"Заказ с Id '{id}' не существует.", nameof(id));
            }

            return order;
        }

        /// <summary>
        /// Получение оценки.
        /// </summary>
        /// <param name="id">Id оценки.</param>
        /// <returns>Оценка.</returns>
        /// <exception cref="ArgumentException"></exception>
        public UserScore GetUserScore(Guid id)
        {
            var score = _dbContext.UserScores.FirstOrDefault(x => x.Id == id);

            if (score == null)
            {
                throw new ArgumentException($"Оценки с Id '{id}' не существует.", nameof(id));
            }

            return score;
        }

        /// <summary>
        /// Добавление пользователя.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        public void AddUser(User user)
        {
            _dbContext.Users.Add(user);

            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Добавление игры.
        /// </summary>
        /// <param name="game">Игра.</param>
        /// <exception cref="ArgumentException"></exception>
        public void AddGame(Game game)
        {
            var genre = game.GenreId;

            if (genre == null)
            {
                throw new ArgumentException($"Жанра с Id '{game.GenreId}' не существует.", nameof(game));
            }

            game.GenreId = genre;
            game.Genre = _dbContext.Genres.FirstOrDefault(x => x.Id == game.GenreId);

            _dbContext.Games.Add(game);

            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Добавление заказа.
        /// </summary>
        /// <param name="order">Заказ.</param>
        /// <exception cref="ArgumentException"></exception>
        public void AddOrder(Order order)
        {
            var game = _dbContext.Games.First(x => x.Id == (Guid)order.GameId);

            if (game == null)
            {
                throw new ArgumentException($"Игры с Id '{order.GameId}' не существет ");
            }

            var user = _dbContext.Users.First(x => x.Id == (Guid)order.UserId);

            if (user == null)
            {
                throw new ArgumentException($"Пользователя с Id '{order.UserId}' не существет ");
            }

            order.Game = game;
            order.User = user;

            _dbContext.Orders.Add(order);

            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Добавление оценки.
        /// </summary>
        /// <param name="score">Оценка.</param>
        /// <exception cref="ArgumentException"></exception>
        public void AddUserScore(UserScore score)
        {
            var game = _dbContext.Games.First(x => x.Id == (Guid)score.GameId);

            if (game == null)
            {
                throw new ArgumentException($"Игры с Id '{score.GameId}' не существет ");
            }

            var user = _dbContext.Users.First(x => x.Id == (Guid)score.UserId);

            if (user == null)
            {
                throw new ArgumentException($"Пользователя с Id '{score.UserId}' не существет ");
            }

            score.Game = game;
            score.User = user;

            _dbContext.UserScores.Add(score);

            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Удаление пользователя.
        /// </summary>
        /// <param name="id">Id пользователя.</param>
        /// <exception cref="ArgumentException"></exception>
        public void DeleteUser(Guid id)
        {
            var user = _dbContext.Users.First(user => user.Id == id);

            if (user == null) 
            {
                throw new ArgumentException($"Пользователя с Id '{id}' не существует.", nameof(id));
            }

            _dbContext.Users.Remove(user);

            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Удаление игры.
        /// </summary>
        /// <param name="id">Id игры.</param>
        /// <exception cref="ArgumentException"></exception>
        public void DeleteGame(Guid id)
        {
            var game = _dbContext.Games.First(x => x.Id == id);

            if (game == null) 
            {
                throw new ArgumentException($"Игра с Id '{id}' не существует.", nameof(id));
            }

            _dbContext.Games.Remove(game);

            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Удаление заказа.
        /// </summary>
        /// <param name="id">Id заказа.</param>
        /// <exception cref="ArgumentException"></exception>
        public void DeleteOrder(Guid id)
        {
            var order = _dbContext.Orders.First(x => x.Id == id);

            if (order == null)
            {
                throw new ArgumentException($"Заказа с Id '{id}' не существует.", nameof(id));
            }

            _dbContext.Orders.Remove(order);

            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Удаление оценки.
        /// </summary>
        /// <param name="id">Id оценки.</param>\
        /// <exception cref="ArgumentException"></exception>
        public void DeleteUserScore(Guid id)
        {
            var score = _dbContext.UserScores.First(x => x.Id == id);

            if (score == null)
            {
                throw new ArgumentException($"Оценки с Id '{id}' не существует.", nameof(id));
            }

            _dbContext.UserScores.Remove(score);

            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Изменение данных оценки.
        /// </summary>
        /// <param name="score">Оценка.</param>
        public void UpdateUserScore(UserScore score)
        {
            var updatedScore = _dbContext.UserScores.First(s => s.Id == score.Id);

            if (updatedScore == null)
            {
                throw new ArgumentException($"Оценки с Id '{score.Id}' не существует.", nameof(score));
            }       

            updatedScore.Score = score.Score;
            updatedScore.User = score.User;
            updatedScore.UserId = score.UserId;
            updatedScore.GameId = score.GameId;
            updatedScore.Game = score.Game;

            _dbContext.Update(updatedScore);

            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Изменение данных игры.
        /// </summary>
        /// <param name="game">Игра.</param>
        /// <exception cref="ArgumentException"></exception>
        public void UpdateGame(Game game)
        {
            var updatedGame = _dbContext.Games.First(g => g.Id == game.Id);

            if (updatedGame == null)
            {
                throw new ArgumentException($"Игра с Id '{game.Id}' не существует.", nameof(game.Id));
            }

            updatedGame.Name = game.Name;
            updatedGame.Genre = game.Genre;
            updatedGame.Cost = game.Cost;
            updatedGame.GenreId = game.GenreId;

            _dbContext.Update(game); 

            _dbContext.SaveChanges(); 
        }

        /// <summary>
        /// Обновление пользователя.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <exception cref="ArgumentException"></exception>
        public void UpdateUser(User user)
        {
            var updateUser = _dbContext.Users.First(g => g.Id == user.Id);

            if (updateUser == null)
            {
                throw new ArgumentException($"Пользователь не существует.", nameof(updateUser));
            }

            updateUser.Name = user.Name;
            updateUser.Login = user.Login;
            updateUser.Password = user.Password;
            updateUser.Phone = user.Phone;

            _dbContext.Update(updateUser);

            _dbContext.SaveChanges();
        }
    }
}
