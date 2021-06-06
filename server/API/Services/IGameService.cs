using API.Models.InputModel;
using API.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IGameService : IDisposable
    {
        Task<List<GameViewModel>> GetGames(int page, int qty);
        Task<GameViewModel> GetGame(int id);
        Task<GameViewModel> InsertGame(GameInputModel game);
        Task UpdateGame (int id, GameInputModel game);
        Task UpdatePrice (int id, decimal price);
        Task DeleteGame (int id);

    }
}
