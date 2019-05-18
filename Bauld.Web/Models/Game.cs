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
    List<string> j;
    
    public int Add(int i, int j)
    {
        return i + j;
    }
    public List<Question> getQuestions(String file){
    var result = new List<Question>();
    using (StreamReader reader = new StreamReader(file)) {
            string line = null;
            while (null != (line = reader.ReadLine())) {
               string[] questionList = line.Split(",");
               var question = new Question {
                   Label = questionList[0],
                   Category = questionList[1],
                   Answer = questionList[2],
               };
               result.Add(question);
               
            }
           return result; 
    }
    
    
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