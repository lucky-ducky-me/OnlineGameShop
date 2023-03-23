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
    }
}
