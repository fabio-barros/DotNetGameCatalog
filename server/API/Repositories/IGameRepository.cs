using API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public interface IGameRepository : IDisposable
    {
        Task<List<Game>> GetGames(int page, int qty);
        Task<Game> GetGame(int id);
        Task<List<Game>> GetGame(string title, string developer);
        Task InsertGame(Game game);
        Task UpdateGame(Game game);
        //Task UpdatePrice(Game price);
        Task DeleteGame(int id);

    }
}
