using AutoMapper;
using HnefataflServer.Games;
using HnefataflServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace HnefataflServer.Controllers
{
    [ApiController]
    public class GamesController
    {

        public GamesController()
        {
        }

        [HttpGet]
        [Route("/api/games")]
        public IActionResult GetAllGames()
        {
            var res = GameStorage.GetGameList();
            Console.WriteLine("Count: " + GameStorage.GetGameList().Count);
            return new OkObjectResult(res);
        }

        [HttpGet]
        [Route("/api/joinGame")]
        public IActionResult GetNewGame([FromQuery] string player)
        {
            var playerGuid = new Guid(player);
            Console.WriteLine("Player " + playerGuid.ToString() + " wants to play hnefatafl");
            var game = GameStorage.GetGameOfPlayer(playerGuid);
            if (game != null)
            {
                return new CreatedResult($"/api/games{game.GameId}", "already playing");
            }
            game = GameStorage.JoinPlayer(playerGuid);
            Console.WriteLine("Game created " + game.GameId);
            Console.WriteLine("Count " + GameStorage.GetGameList().Count());
            return new CreatedResult($"/api/games{game.GameId}", new GameDto(game.GameId, game.Player1));
        }


        [HttpGet]
        [Route("/api/playMove")]
        public IActionResult GetNewGame([FromBody] Dictionary<string, string> body)
        {
            //Play a move
            return new OkResult();
        }
    }
}
