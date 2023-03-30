using DataBaseProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider
{
    public interface IOnlineGameShopProvider
    {
        public IEnumerable<User> GetAllUsers();

        public IEnumerable<Genre> GetAllGenres();

        public IEnumerable<Game> GetAllGames();

        public IEnumerable<Order> GetAllOrders();

        public IEnumerable<UserScore> GetAllUsersScore();

        public User GetUser(Guid id);

        public Genre GetGenre(Guid id);

        public Game GetGame(Guid id);   

        public Order GetOrder(Guid id);

        public UserScore GetUserScore(Guid id);
    }
}
