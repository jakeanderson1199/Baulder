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
    public Boolean Start {get; set;}
    public Dictionary<string,Player> PlayerDict {get; set;} = new Dictionary<string,Player>();
    public Turn Turn {get; set;} = new Turn();
    public string GameID {get; set;}

    public void NewTurn(Question q){
        this.Turn.CurrentQuestion = q;
        if (this.Turn.Answers != null) {
            this.Turn.Answers.Clear();
        }
        if (this.Turn.Votes != null){
            this.Turn.Votes.Clear();
        }

    }
    public void AddPlayer(Player p) {
        p.PlayerID = System.Guid.NewGuid().ToString();
        this.PlayerDict.Add(p.PlayerID,p);
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