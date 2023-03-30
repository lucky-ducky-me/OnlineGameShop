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

        public Task<Tuple<bool, Guid>> AddUser(User user);

        public Task<Tuple<bool, Guid>> AddGenre(Genre genre);

        public Task<Tuple<bool, Guid>> AddGame(Game game);

        public Task<Tuple<bool, Guid>> AddOrder(Order order);

        public Task<Tuple<bool, Guid>> AddUserScore(UserScore score);
    }
}
