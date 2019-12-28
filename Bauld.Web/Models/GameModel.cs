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
    public class GameModel{
    public string OwnerName {get; set;}
    //public Boolean Start {get; set;}
    public List<Player> Players {get; set;} = new List<Player>();
    public Turn Turn {get; set;} = new Turn();
    public string GameID {get; set;}
    public Boolean AllAnswered {get; set;}

    
    }
}