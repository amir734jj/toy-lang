using FParsec.CSharp;
using FParsecParser;
using Models;
using Xunit;

namespace ParserTests
{
    public class NotEqualsTest
    {
        [Theory]
        [InlineData("foo!=bar")]
        [InlineData(" foo!=bar")]
        [InlineData("foo!=bar ")]
        [InlineData(" foo!=bar ")]
        [InlineData("foo != bar")]
        [InlineData(" foo != bar")]
        [InlineData("foo != bar ")]
        [InlineData(" foo != bar ")]
        public void Test_NotEquals(string text)
        {
            // Act
            var reply = Parser.Expression().ParseString(text);

            // Assert
            Assert.True(reply.IsOk());
            Assert.Equal(new NotEqualsToken(new VariableToken("foo"), new VariableToken("bar")), reply.Result);
        }
    }
}