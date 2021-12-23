using AntlrParser;
using FParsecParser;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Core.Tests
{
    public class ParserTest
    {
        [Fact]
        public void Test_Basic()
        {
            // Arrange, Act
            var ast1 = new ToyAntlrParser(NullLogger<ToyAntlrParser>.Instance)
                .Parse(ToyCompiler.BasicFileText);
            var ast2 = new ToyFparsecParser(NullLogger<ToyFparsecParser>.Instance)
                .Parse(ToyCompiler.BasicFileText);
            
            // Assert
            Assert.Equal(ast1, ast2);
        }
    }
}