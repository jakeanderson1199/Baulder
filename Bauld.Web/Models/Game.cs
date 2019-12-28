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
    public class Game{
    public string OwnerName {get; set;}
    //public Boolean Start {get; set;}
    public Dictionary<string,Player> PlayerDict {get; set;} = new Dictionary<string,Player>();
    public Turn Turn {get; set;} = new Turn();
    public string GameID {get; set;}
    public Boolean AllAnswered => GetAllAnswered();

    public Boolean GetAllAnswered(){
        if (PlayerDict.Count == Turn.Answers.Count-1){
            return true;
        }
        else {
            return false;
        }
    }

    public void NewTurn(Question q){
        this.Turn.CurrentQuestion = q;
        if (this.Turn.Answers != null) {
            this.Turn.Answers.Clear();
        }
        if (this.Turn.Votes != null){
            this.Turn.Votes.Clear();
        }
        Answer RealAnswer = new Answer {
            UserName = "REAL_ANSWER",
            Text = this.Turn.CurrentQuestion.Answer, 
            PlayerID = "REAL_ANSWER"
            };
        this.Turn.Answers.Add(RealAnswer);
        foreach(Player p in PlayerDict.Values){
            p.Clear();
        }

    }
    public void AddPlayer(Player p) {
        p.PlayerID = System.Guid.NewGuid().ToString();
        this.PlayerDict.Add(p.PlayerID,p);
    }
    public Player GetPlayerByName(String name){
        return PlayerDict.Values.FirstOrDefault(p => p.Name == name);
    }
    

    
    
public void Collections() {

    var ageMap = new Dictionary<string, int>();

    ageMap.Add("jake", 12);

    ageMap["pter"] = 7;
    bool old = ageMap["jake"] > 3;

    var list = new List<string>();

    list.Add("hey");

    list = new List<string> {
        "a", "b", "c"
    };

    list.ForEach(l => {
        Console.WriteLine(l);
    });

    var  b = list.FirstOrDefault(l => l == "b");

    var moreThanB = list.Where(l => l == "b" || l == "c");
}    
    }


    
}