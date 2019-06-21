using System.Linq;
using System.Collections.Generic;
namespace Models
{
    public class GameRepository
    {
        private Dictionary<string,Game> Games { get; set; } = new Dictionary<string, Game>();
        public Game AddGame(string Owner){
            var g = new Game{OwnerName = Owner, GameID = System.Guid.NewGuid().ToString()};
            Games.Add(g.GameID, g);
            return g;
     }   
      public Game GetGame(string gameID){
          var g = Games[gameID];
            return g;
     } 
     public List<Game> GetGames(){
          var list = Games.Values.ToList();
            return list;
     } 
    }
}