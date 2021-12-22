using FParsec.CSharp;
using FParsecParser;
using Models;
using Xunit;

namespace ParserTests
{
    public class LessThanEqualsParserTest
    {
        [Theory]
        [InlineData("1<=2")]
        [InlineData(" 1<=2")]
        [InlineData("1<=2 ")]
        [InlineData(" 1<=2 ")]
        public void Test_Atomic(string text)
        {
            // Act
            var reply = Parser.Expression().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            Assert.Equal(
                new LessThanEqualsToken(
                    new AtomicToken(1),
                    new AtomicToken(2)), reply.Result);
        }
    }
}