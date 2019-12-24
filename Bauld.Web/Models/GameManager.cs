using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
namespace Models
{
    public class GameManager{
        private QuestionRepository questionRepository;
        private GameRepository gameRepository;
        public GameManager(QuestionRepository questionRepository, GameRepository gameRepository){
            this.questionRepository = questionRepository;
            this.gameRepository = gameRepository;
        }
        public Question NextQuestion() {
            return questionRepository.getNextQuestion();
        }
        public List<Game> GetGames(){
            return gameRepository.GetGames();
        }
        public void CountPoints(string gameID) {
            var g = GetGame(gameID);
            g.Turn.Votes.ForEach(v => {
               if (v.CorrectAnswer) {
                   g.PlayerDict[v.PlayerId].Points += 2;          // 2 points for choosing the actual answer to the question
               }
               else if (g.PlayerDict[v.AnswerId] != null) {
                   g.PlayerDict[v.AnswerId].Points++;            // 1 point for every time someone else votes for your answer
               }
            });
        }
        public Game AddGame(string Owner) {
            var g = new Game{OwnerName = Owner, GameID = System.Guid.NewGuid().ToString()}; 

            var q = this.NextQuestion();
            g.NewTurn(q);
            this.gameRepository.AddGame(g);

            

            return g;
        }
        public void Reset() {
            gameRepository.Reset();
        }
        public Game GetGame(string gameID){
            var g = gameRepository.GetGame(gameID);
            return g;
        }
        
    }
}