//using API.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace API.Repositories
//{
//    public class GameRepository : IGameRepository
//    {

//        private static Dictionary<Guid, Game> games = new()
//        {

//        };


//        public Task<List<Game>> GetGames(int page, int qty)
//        {
//            return Task.FromResult(games.Values.Skip((page - 1) * qty).Take(qty).ToList());
//        }

//        public Task<Game> GetGame(Guid id)
//        {
//                if (!games.ContainsKey(id))
//                {
//                    return null;
//                }

//                return Task.FromResult(games[id]);
//         }

//        public Task<List<Game>> GetGame(string title, string developer)
//        {
//            return Task.FromResult(games.Values.Where(game => game.Title.Equals(title) && game.Developer.Equals(developer)).ToList());
//        }

//        public Task InsertGame(Game game)
//        {
//            games.Add(game.Id, game);

//            return Task.CompletedTask;
//        }

//        public Task UpdateGame(Game game)
//        {
//            games[game.Id] = game;
//            return Task.CompletedTask;
//        }

//        public Task DeleteGame(Guid id)
//        {
//            games.Remove(id);
//            return Task.CompletedTask;
//        }

//        public void Dispose()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
