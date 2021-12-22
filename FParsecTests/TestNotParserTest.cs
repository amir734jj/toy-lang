using FParsec.CSharp;
using FParsecParser;
using Models;
using Xunit;

namespace ParserTests
{
    public class TestNotParserTest
    {
        [Theory]
        [InlineData("!x")]
        [InlineData(" !x")]
        [InlineData("!x ")]
        [InlineData(" !x ")]
        public void Test_Atomic(string text)
        {
            // Act
            var reply = Parser.Expression().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            Assert.Equal(
                new NotToken(
                    new VariableToken("x")), reply.Result);
        }
    }
}