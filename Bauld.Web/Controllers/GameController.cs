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
        public GameController(GameManager gameManager, QuestionRepository questionRepository) {
            this.gameManager = gameManager;
            this.questionRepository = questionRepository;
        }

        [HttpGet]
        [Route("api/games/{gameId}")]
        public IActionResult GetGame(string gameId) {
            var game = gameManager.GetGame(gameId);
            return Ok(game);
        }
        [HttpGet]
        [Route("api/games")]
        public IActionResult GetGames() {
            var g = gameManager.GetGames();
            List<GameSummary> games = new List<GameSummary>();
            g.ForEach(g => {})
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
        public IActionResult AddPlayer([FromBody] Player player, String gameId) {
            Game g = this.gameManager.GetGame(gameId);
            g.AddPlayer(player);
            return Ok(player);
        }

        [HttpPost]
        [Route("api/games/{gameId}/turns")]
        public IActionResult NewTurn(String gameId) {
            Game g = this.gameManager.GetGame(gameId);
            Question q = this.gameManager.NextQuestion();
            g.NewTurn(q);
            return Ok(g);
        }
        [HttpPost]
        [Route("api/games/{gameId}/answers")]
        public IActionResult PostAnswer(String gameId, [FromBody] Answer a) {
            Game g = this.gameManager.GetGame(gameId);
            g.Turn.Answers.Add(a);
            return Ok(g);
        }
        [HttpPost]
        [Route("api/games/{gameId}/votes")]
        public IActionResult PostVote(String gameId, [FromBody] Answer a) {
            Game g = this.gameManager.GetGame(gameId);
            g.Turn.Answers.Add(a);
            return Ok(g);
        }
        [HttpPost]
        [Route("api/games/{gameId}/votes/points")]
        public IActionResult PostPoints(String gameId) {
            Game g = this.gameManager.GetGame(gameId);
            gameManager.CountPoints(gameId);
            return Ok(g);
        }
        

        
    }
}
