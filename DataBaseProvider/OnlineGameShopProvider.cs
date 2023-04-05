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

        public IEnumerable<Game> GetAllGames()
        {
            return _dbContext.Games.ToList();
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            return _dbContext.Genres.ToList();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _dbContext.Orders.ToList();
        }

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

        public User GetUser(Guid id)
        {
            var users = _dbContext.Users;

            if (users == null)
            {
                throw new Exception();
            }

            var user = users.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                throw new Exception();
            }

            return user;
        }

        public Genre GetGenre(Guid id)
        {
            var genres = _dbContext.Genres;

            if (genres == null)
            {
                throw new Exception();
            }

            var genre = genres.FirstOrDefault(x => x.Id == id);

            if (genre == null)
            {
                throw new Exception();
            }
            
            return genre;
        }

        public Game GetGame(Guid id)
        {
            var games = _dbContext.Games;
           
            if (games == null)
            {
                throw new Exception();
            }

            var game = games.FirstOrDefault(x => x.Id == id);

            if (game == null)
            {
                 throw new Exception();
            }

            return game;
        }

        public Order GetOrder(Guid id)
        {
            throw new NotImplementedException();
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

        public bool AddUser(User user)
        {
            _dbContext.Users.Add(user);

            return _dbContext.SaveChanges() > 0;
        }

        public bool AddGame(Game game)
        {
            _dbContext.Games.Add(game);

            return _dbContext.SaveChanges() > 0;
        }

        public bool AddOrder(Order order)
        {
            throw new NotImplementedException();
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

        public bool DeleteUser(Guid id)
        {
            _dbContext.Users.Remove(new User { Id = id });

            return _dbContext.SaveChanges() > 0;
        }

        public bool DeleteGame(Guid id)
        {
            _dbContext.Games.Remove(new Game() { Id = id});

            return _dbContext.SaveChanges() > 0;
        }

        public bool DeleteOrder(Guid id)
        {
            throw new NotImplementedException();
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

            _dbContext.UserScores.Remove(new UserScore { Id = id });

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
    }
}
