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
    public class OnlineGameShopProvider : IOnlineGameShopProvider
    {
        private OnlineGameShopContext _dbContext;

        internal string ConnectionString { get; set; }

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

        public UserScore GetUserScore(Guid id)
        {
            var scores = _dbContext.UserScores;

            if (scores == null)
            {
                throw new Exception();
            }

            var score = scores.FirstOrDefault(x => x.Id == id);

            if (score == null)
            {
                throw new Exception();
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

        public bool AddUserScore(UserScore score)
        {
            _dbContext.UserScores.Add(score);

            return _dbContext.SaveChanges() > 0;
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

        public bool DeleteUserScore(Guid id)
        {
            _dbContext.UserScores.Remove(new UserScore { Id = id });

            return _dbContext.SaveChanges() > 0;
        }
    }
}
