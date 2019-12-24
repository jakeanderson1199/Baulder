using System;
namespace Models {
    public class Player{
       public string Name{get; set;}
       public int Points{get; set;}
       public Answer Answer{get; set;}
       public Boolean Owner{get; set;}
       public string PlayerID{get;set;}
       public void Clear(){
           this.Answer = null;
       }
       
    }
}