using System.Linq;
using System.Collections.Generic;
namespace Models
{
    public class GameRepository
    {
        private Dictionary<string,Game> Games { get; set; } = new Dictionary<string, Game>();
        public Game AddGame(Game g){
            Games.Add(g.GameID, g);
            return g;
     }   
      public Game GetGame(string gameID){
         Games.TryGetValue(gameID, out Game g);
            return g;
     } 
     public List<Game> GetGames(){
          var list = Games.Values.ToList();
            return list;
     }
     public void Reset(){
         Games.Clear();
     } 
    }
}