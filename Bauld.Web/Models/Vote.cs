using System;
namespace Models {
    public class Vote {
        public string AnswerId {get; set;}  //This is the PlayerID of the person who wrote the answer
        public string PlayerId {get; set;}  //This is the PlayerID of the person who voted for the answer specified above
        public Boolean CorrectAnswer {get; set;}
    }
}