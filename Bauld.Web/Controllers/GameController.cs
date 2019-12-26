using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace bauld.Controllers
{
    public class GameController : Controller
    {
private GameManager gameManager;
private QuestionRepository questionRepository;
private GameModel GameToGameModel(Game game){
    var playerList = game.PlayerDict.Values.ToList();
    var gameModel = new GameModel{
    OwnerName = game.OwnerName, Players = playerList, Turn = game.Turn, GameID = game.GameID
                
    };
    return gameModel;
}
        public GameController(GameManager gameManager, QuestionRepository questionRepository) {
            this.gameManager = gameManager;
            this.questionRepository = questionRepository;
        }

        [HttpGet]
        [Route("api/games/{gameId}")]
        public IActionResult GetGame(string gameId) {
            var game = gameManager.GetGame(gameId);
            var gameModel = this.GameToGameModel(game);
            return Ok(gameModel);
        }
        [HttpGet]
        [Route("api/games")]
        public IActionResult GetGames() {
            var g = gameManager.GetGames();
            List<GameSummary> games = new List<GameSummary>();
            g.ForEach(game => {
                var gameSummary = new GameSummary();
                gameSummary.GameId = game.GameID;
                gameSummary.OwnerName = game.OwnerName;
                games.Add(gameSummary);
            });
            return Ok(games);
        }


        [HttpPost]
        [Route("api/games/{ownerName}")]
        public IActionResult AddGame(string ownerName) {
            this.gameManager.AddGame(ownerName);

            return Ok();
        }

        [HttpPost]
        [Route("api/games/{gameId}/players")]
        public IActionResult AddPlayer([FromBody] AddPlayerRequest player, String gameId) {
            Game g = this.gameManager.GetGame(gameId);
            if (g == null) {
                return UnprocessableEntity(new { message = $"Game id {gameId} not found"});
            }

            Player p = new Player {Name = player.Name};
            if (g.OwnerName == p.Name){
                p.Owner = true;
            }
            g.AddPlayer(p);
            return Ok(player);
        }

        [HttpPost]
        [Route("api/games/{gameId}/turns")]
        public IActionResult NewTurn(String gameId) {              //TODO remove players answers
            Game g = this.gameManager.GetGame(gameId);
            Question q = this.gameManager.NextQuestion();
            g.NewTurn(q);
            var gameModel = this.GameToGameModel(g);
            return Ok(g);
        }
        [HttpPost]
        [Route("api/games/{gameId}/players/{user}/answer")]
        public IActionResult PostAnswer(String gameId, String user, [FromBody] Answer a) {
            Game g = this.gameManager.GetGame(gameId);
            g.Turn.Answers.Add(a);
            g.GetPlayerByName(user).Answer = a;
            a.PlayerID = g.GetPlayerByName(user).PlayerID;
            var gameModel = this.GameToGameModel(g);
            return Ok(gameModel);
        }
        [HttpPost]
        [Route("api/games/{gameId}/players/{user}/vote")]
        public IActionResult PostVote(String gameId, String user, [FromBody] String PlayerID) {
            Game g = this.gameManager.GetGame(gameId);
            Vote v = new Vote();
            v.AnswerId = PlayerID;  // gets the PlayerID of the player who wrote the answer that the user voted for
            v.PlayerId = g.GetPlayerByName(user).PlayerID;        // gets the PlayerID of the player who is voting for the answer
            g.Turn.Votes.Add(v);
            var gameModel = this.GameToGameModel(g);
            return Ok(gameModel);
        }
        [HttpPost]
        [Route("api/games/{gameId}/votes/points")]
        public IActionResult PostPoints(String gameId) {
            Game g = this.gameManager.GetGame(gameId);
            gameManager.CountPoints(gameId);
            return Ok(g);
        }

        [HttpPost]
        [Route("api/reset")]
        public IActionResult resetState() {
            gameManager.Reset();
            return Ok();
        }
        

        
    }
}
