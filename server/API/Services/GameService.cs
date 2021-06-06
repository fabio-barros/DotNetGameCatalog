using API.Entities;
using API.Exceptions;
using API.Models.InputModel;
using API.Models.ViewModel;
using API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }


        //GET:
        public async Task<List<GameViewModel>> GetGames(int page, int qty)
        {
            var res = await _gameRepository.GetGames(page, qty);

            return res.Select(game => new GameViewModel
            {
                Id = game.Id,
                Title = game.Title,
                Developer = game.Developer,
                Publisher = game.Publisher,
                Year = game.Year,
                Price = game.Price

            }).ToList();
        }

        //GET:id
        public async Task<GameViewModel> GetGame(int id)
        {
            var game = await _gameRepository.GetGame(id);

            if(game == null)
            {
                return null;
            }

            return new GameViewModel
            {
                Id = game.Id,
                Title = game.Title,
                Developer = game.Developer,
                Publisher = game.Publisher,
                Year = game.Year,
                Price = game.Price

            };
        }

        //POST:
        public async Task<GameViewModel> InsertGame(GameInputModel game)
        {
            var gameEntity = await _gameRepository.GetGame(game.Title, game.Developer);

            if(gameEntity.Count > 0)
            {
                throw new GameAlreadyExistsException();
            }

            var newInsert = new Game
            {
                Title = game.Title,
                Developer = game.Developer,
                Publisher = game.Publisher,
                Year = game.Year,
                Price = game.Price

            };

            await _gameRepository.InsertGame(newInsert);

            return new GameViewModel
            {
                Id = newInsert.Id,
                Title = newInsert.Title,
                Developer = newInsert.Developer,
                Publisher = newInsert.Publisher,
                Year = newInsert.Year,
                Price = newInsert.Price
            };

        }

        //PUT
        public async Task UpdateGame(int id, GameInputModel game)
        {
            var gameEntity = await _gameRepository.GetGame(id);

            if (gameEntity == null)
            {
                throw new GameDoesNotExistException();
            }

            gameEntity.Title = game.Title;
            gameEntity.Developer = game.Developer;
            gameEntity.Publisher = game.Publisher;
            gameEntity.Year = game.Year;
            gameEntity.Price = game.Price;

            await _gameRepository.UpdateGame(gameEntity);

        }

        //PUT {price}
        public async Task UpdatePrice(int id, decimal price)
        {
            var gameEntity = await _gameRepository.GetGame(id);

            if (gameEntity == null)
            {
                throw new GameDoesNotExistException();
            }

            gameEntity.Price = price;

            await _gameRepository.UpdateGame(gameEntity);
        }

        //DELETE
        public async Task DeleteGame(int id)
        {
            var game = await _gameRepository.GetGame(id);

            if(game== null)
            {
                throw new GameDoesNotExistException();
            }

            await _gameRepository.DeleteGame(id);
        }

        public void Dispose()
        {
            _gameRepository?.Dispose();
        }
    }
}
