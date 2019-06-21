using System;
using System.Collections.Generic;
using Xunit;
using Models;
namespace Bauld.Tests
{
    public class GameTests
    {
        [Fact]
        public void TestGetQuestions()
        {
            var list = new List<Question>{new Question{Label = "test"}};
            var q = new QuestionRepository(list);
            var result = q.getNextQuestion();
            Assert.Equal("test",result.Label);
        }
    }
}
