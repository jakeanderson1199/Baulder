using System.Collections.Generic;
namespace Models {
    public class Turn{
       public List<Answer> Answers{get; set;} = new List<Answer>();
       public List<Vote> Votes{get; set;} = new List<Vote>(); 
       public Question CurrentQuestion {get; set;}
       
    }
}