using DataBaseProvider.Models;
using Microsoft.AspNetCore.Identity;

namespace OnlineGameShopApi.SerializationModels
{
    /// <summary>
    /// Класс для преобразования сущностей БД в модели и наоборот.
    /// </summary>
    public static class Transform
    {
        /// <summary>
        /// Преобразование жанра из сущности БД в модель для ответа.
        /// </summary>
        /// <param name="genre">Сущность БД.</param>
        /// <returns>Модель для ответа.</returns>
        internal static GenreDataResponse TransformToGenreResponse(Genre genre)
        {
            if (genre == null)
            {
                return null;
            }

            return new GenreDataResponse { Id = genre.Id, GenreName = genre.GenreName };
        }

        /// <summary>
        ///  Преобразование игры из сущности БД в модель для ответа.
        /// </summary>
        /// <param name="game">Сущность БД.</param>
        /// <returns>Модель для ответа.</returns>
        internal static GameDataResponse TransformToGameResponse(Game game)
        {
            if (game == null)
            {
                return null;
            }

            var genreResponse = TransformToGenreResponse(game.Genre);

            return new GameDataResponse
            {
                Id = game.Id
                ,
                Genre = genreResponse
                ,
                Cost = game.Cost
                ,
                Name = game.Name
            };
        }


        /// <summary>
        /// Преобразование пользователя из сущности БД в модель.
        /// </summary>
        /// <param name="user">Сущность БД.</param>
        /// <returns>Модель для ответов.</returns>
        internal static UserData TransformToUserData(User user)
        {
            if (user == null)
            {
                return null;
            }

            return new UserData
            {
                Id = user.Id,
                Name = user.Name,
                Login = user.Login,
                Phone = user.Phone,
                Password = null!
            };
        }

        /// <summary>
        /// Преобразование оценки из сущности БД в модель для ответа.
        /// </summary>
        /// <param name="userScore">Сущность БД.</param>
        /// <returns>Модель для ответа.</returns>
        internal static UserScoreDataResponse TransformToUserScoreDataResponse(UserScore userScore)
        {
            var gameData = Transform.TransformToGameResponse(userScore.Game);

            var userData = Transform.TransformToUserData(userScore.User);

            var userScoreDataResponse = new UserScoreDataResponse
            {
                Id = userScore.Id,
                GameId = (Guid)userScore.GameId,
                UserId = (Guid)userScore.UserId,
                Score = userScore.Score,
                Game = gameData,
                User = userData
            };

            return userScoreDataResponse;
        }

        /// <summary>
        /// Преобразование заказа из сущности БД в модель для ответа.
        /// </summary>
        /// <param name="order">Сущность БД.</param>
        /// <returns>Модель для ответа.</returns>
        internal static OrderDataResponse TransformToOrderDataResponse(Order order)
        {
            var gameData = TransformToGameResponse(order.Game);

            var userData = TransformToUserData(order.User);

            var orderDataResponse = new OrderDataResponse
            {
                Id = order.Id,
                GameId = (Guid)order.GameId,
                UserId = (Guid)order.UserId,
                Game = gameData,
                User = userData
            };

            return orderDataResponse;
        }
    }
}
