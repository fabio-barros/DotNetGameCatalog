using API.Exceptions;
using API.Models.InputModel;
using API.Models.ViewModel;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // GET:
        [HttpGet]
        public async Task<ActionResult<List<GameViewModel>>> GetGames([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int qty = 5)
        {
            var res = await _gameService.GetGames(page, qty);

            if (res.Count == 0)
            {
                return NoContent();
            }
               

            return Ok(res);
        }

        // GET: id
        [HttpGet("{gameId:int}")]
        public async Task<ActionResult<GameViewModel>> GetGame([FromRoute] int gameId)
        {
            var res = await _gameService.GetGame(gameId);

            if (res == null)
            {
                return NoContent();
            }
            return Ok(res);
        }


        // POST: 
        [HttpPost]
        public async Task<ActionResult<GameViewModel>> PostGame([FromBody] GameInputModel gameInputModel)
        {
            try
            {
                var res = await _gameService.InsertGame(gameInputModel);
                return Ok(res);

            }
            //GameAlreadyExistsException
            catch (GameAlreadyExistsException e)
            {

                return UnprocessableEntity("Game with the same name and from the same developer already exists.");
            }

        }

        // PUT:
        [HttpPut("{gameId:int}")]
        public async Task<IActionResult> PutGame([FromRoute] int gameId, [FromBody] GameInputModel gameInputModel)
        {
            try
            {
                await _gameService.UpdateGame(gameId, gameInputModel);
                return Ok();

            }
            //GameDoesNotExistException
            catch (GameDoesNotExistException e)
            {
                return NotFound("Game does not exist");
            }
        }

        // PATCH:
        [HttpPatch("{gameId:int}/price/{price:decimal}")]
        public async Task<IActionResult> PatchGame([FromRoute] int gameId,[FromRoute] decimal price)
        {
            try
            {
                await _gameService.UpdatePrice(gameId, price);
                return Ok();

            }

            //GameDoesNotExist
            catch (GameDoesNotExistException e)
            {
                return NotFound("Game does not exist");
            }
        }


        // DELETE: 
        [HttpDelete("{gameId:int}")]
        public async Task<IActionResult> DeleteGame([FromRoute] int gameId)
        {
            try
            {
                await _gameService.DeleteGame(gameId);
                return Ok();

            }

            //GameDoesNotExist
            catch (GameDoesNotExistException e)
            {
                return NotFound("Game does not exist");
            }

        }

        //private bool FuncionarioExists(int id)
        //{
        //    return Ok();
        //    //return _context.Game.Any(e => e.GameId == id);
        //}

    }
}
