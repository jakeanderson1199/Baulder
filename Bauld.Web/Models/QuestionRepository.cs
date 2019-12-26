using  System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
namespace Models
{
    public class QuestionRepository
    {
        public QuestionRepository() {
            Questions = this.getQuestions(@"Models/questions.txt");
        }
        public QuestionRepository(List<Question> list) {
            list.ForEach(q => Questions.Push(q));
        }
        private Stack<Question> Questions {get; set;} = new Stack<Question>();
        private Stack<Question> getQuestions(String file)
        {
            var result = Questions;
            using (StreamReader reader = new StreamReader(file))
            {
                string line = null;
                while (null != (line = reader.ReadLine()))
                {
                    string[] questionStack = line.Split(",");
                    var question = new Question
                    {
                        Label = questionStack[0],
                        Category = questionStack[1],
                        Answer = questionStack[2],
                    };
                    result.Push(question);

                }
                return result;
            }


        }
        public Question getNextQuestion()
        {
            if (Questions.Count == 0) {
                this.Questions = this.getQuestions(@"Models\questions.txt");
            };
            return Questions.Pop();
        }


        }
    }
    