using DataBaseProvider.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider
{
    public class OnlineGameShopProvider : IOnlineGameShopProvider
    {
        private OnlineGameShopContext _dbContext;

        internal string ConnectionString { get; set; }

        public OnlineGameShopProvider(string connectionString) 
        {
            _dbContext = new OnlineGameShopContext(connectionString);
        }

        public IEnumerable<Game> GetAllGames()
        {
            return _dbContext.Games.ToList();
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserScore> GetAllUsersScore()
        {
            throw new NotImplementedException();
        }

        User IOnlineGameShopProvider.GetUser(Guid id)
        {
            throw new NotImplementedException();
        }

        Genre IOnlineGameShopProvider.GetGenre(Guid id)
        {
            throw new NotImplementedException();
        }

        Game IOnlineGameShopProvider.GetGame(Guid id)
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

        Order IOnlineGameShopProvider.GetOrder(Guid id)
        {
            throw new NotImplementedException();
        }

        UserScore IOnlineGameShopProvider.GetUserScore(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
