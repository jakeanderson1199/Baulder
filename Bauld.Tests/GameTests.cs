using System;
using Xunit;
using Models;
namespace Bauld.Tests
{
    public class GameTests
    {
        [Fact]
        public void TestGetQuestions()
        {
            var game = new Game();
            var result = game.getQuestions(@"C:\Dev\bauld\Bauld.Web\Models\questions.txt");
            Assert.Equal(4,result.Count);
        }
    }
}
